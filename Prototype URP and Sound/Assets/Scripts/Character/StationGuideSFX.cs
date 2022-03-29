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
    [Range(0.5f, 10.0f)]
    private float radius = 6;
    private float proximity;
    #endregion

    void Start()
    {
        // Start the looping event and set proximity (volume) to 0.
        proxSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/Station Proximity 2D");
        proxSFX.setParameterByName("ProximityToStation", 0);
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
            proximity = Vector3.Distance(this.transform.position, StationManager.instance.DetectedStationTrigger().transform.position);
        }
        else
        {
            proximity = 100000000;
        }
        
        return proximity;
    }

    private void SetVolume()
    {
        if(CalculateProximity() <= radius)
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
