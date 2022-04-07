using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a sound when the player gets close to a station while holding its corresponding photo.
/// Volume scales with proximity.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class StationGuideSFX : MonoBehaviour
{
    /* Requirements:
     * 1. Determine when we are within a specific radius of the station of the currently held photo
     * 2. Unmute the sound while 1 is true (it is always playing regardless)
     * 3. Scale sound with radius (closer = louder)
     */

    #region Fields
    private FMOD.Studio.EventInstance proxSFX;
    [SerializeField]
    private float radius = 3;
    [SerializeField]
    private float proximity;

    private Vector3 playerLoc = Vector3.zero;
    private Vector3 stationLoc = Vector3.zero;
    #endregion

    void Start()
    {
        // Start the looping event and set proximity (volume) to 0.
        proxSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/Station Proximity 2D");
        proxSFX.setParameterByName("ProximityToStation", 0);
        proxSFX.start();
        proxSFX.release();
    }

    void Update()
    {
        SetVolume();
    }

    #region Helper Methods
    /// <summary>
    /// Determines whether the player is within the given radius of the current photo's station.
    /// </summary>
    /// <returns>True if within radius; false otherwise.</returns>
    private float CalculateProximity()
    {
        // StationManager's station list will only ever have a station in it while the player is inside that station's trigger box.
        // Assume that while the list is empty, the player is not close enough to any station for the sound to trigger.
        if(StationManager.instance.DetectedStationTrigger() != null)
        {
            playerLoc.x = this.transform.position.x;
            playerLoc.z = this.transform.position.z;
            stationLoc.x = StationManager.instance.DetectedStationTrigger().transform.position.x;
            stationLoc.z = StationManager.instance.DetectedStationTrigger().transform.position.z;

            proximity = Vector3.Distance(playerLoc, stationLoc);
        }
        else
        {
            proximity = 100000000;
        }
        return proximity;
    }

    /// <summary>
    /// Determines the radius of the current station's trigger box.
    /// </summary>
    /// <returns>Station trigger box radius if available, -1 if not.</returns>
    private float CalculateRadius()
    {
        if (StationManager.instance.DetectedStationTrigger() != null)
        {
            radius = StationManager.instance.DetectedStationTrigger().GetComponent<BoxCollider>().size.x / 2;
        }
        else
        {
            radius = -1;
        }
        return radius;
    }

    private void SetVolume()
    {
        if(CalculateProximity() <= CalculateRadius())
        {
            // some inverse of distance to station trigger: | distance / radius - 1 |
            proxSFX.setParameterByName("ProximityToStation", Mathf.Abs(proximity / radius - 1));
        }
        else
        {
            proxSFX.setParameterByName("ProximityToStation", 0);
        }
    }
    #endregion
}
