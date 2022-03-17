using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StationInfo
{
    public bool success;
    public float glow;
    public float vignette;
    public float pulse;

    public StationInfo(bool _success, float _glow, float _vignette, float _pulse)
    {
        success = _success;
        glow = _glow;
        vignette = _vignette;
        pulse = _pulse;
    }
}

/// <summary>
/// Used to check and react to player's action and transform, and control objects, materials, and probably many other things to change based on the state of the Locus.
/// </summary>
/// <author>
/// Alfie Luo
/// </author>
public class Station : MonoBehaviour
{
    public enum State
    {
        present,
        past
    }

    [Header("ID")]
    //ID used to match photos
    public string id;

    [Header("Objects")]
    //Object lists
    public List<GameObject> past;
    public List<GameObject> present;

    //Trigger that activates the station
    [Header("Activation Box")]
    public GameObject trigger;

    //radius of the correct location
    [Header("Correct Radius")]
    public float radius;

    //correct rotation
    [Header("Rotation")]
    public Vector3 rot;

    //the bigger the easier to align
    [Header("Tolerance")]
    [SerializeField]
    [Range(1f, 5.0f)]
    private float tolerance_rot = 5.0f;
    private float angleDifference;

    //Controlls the vignette effect, the bigger the easier for it to appear
    [Header("Hint - Vignette Angle")]
    [SerializeField]
    [Range(15f, 90f)]
    private float vig_rot = 25.0f;
    public float vignetteScalar = 0.75f;

    [Header("Glow Curve")]
    public AnimationCurve glow_Curve;

    [Header("Other")]
    public State state; //current state
    public bool isSingleUse; //destory component after a toggle

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Class initializaiton
    /// </summary>
    private void Init()
    {
        //rot = this.transform.rotation.eulerAngles;
        state = this.state;
        isSingleUse = this.isSingleUse;

        //vignette = postProcessProfile.GetSetting<Vignette>();
    }

    /// <summary>
    /// Helper function, Toggle the state of the current locus between past and present, this method exists be cause locus only has two states.
    /// </summary>
    public void ToggleState()
    {
        if (state == State.past)
        {
            SwitchState(State.present);
        }
        else
        {
            SwitchState(State.past);
        }

        if (isSingleUse)
            Destroy(GetComponent<Station>());
    }

    /// <summary>
    /// Helper function, switch the current state of this locus to a specified state.
    /// </summary>
    /// <param name="s">The specified state the locus will switch to.</param>
    private void SwitchState(State s)
    {
        switch (s)
        {
            case State.past:
                if (present.Count > 0)
                {
                    foreach (GameObject g in present)
                        g.SetActive(false);
                }
                if (past.Count > 0)
                {
                    foreach (GameObject g in past)
                        g.SetActive(true);
                }
                break;
            case State.present:
                if (present.Count > 0)
                {
                    foreach (GameObject g in present)
                        g.SetActive(true);
                }
                if (past.Count > 0)
                {
                    foreach (GameObject g in past)
                        g.SetActive(false);
                }
                break;
        }

        state = s;
    }

    /// <summary>
    /// Check if player is holding the correct photo and if player's current rotation and position is correct. If correct, toggle state.
    /// </summary>
    public StationInfo CheckPlayer()
    {
        bool success = false;
        float glow = 0f, vignette = 0f, pulse = 0f;

        // #1 Check Photo ID
        if (CharacterComponents.instance.heldPhoto.heldPhoto != null && CharacterComponents.instance.heldPhoto.heldPhoto.ID == id)
        {

            // I think we should move anything that changes the player's photo from here to either HeldPhoto or PhotoController.
            // We can try to call an event here instead that the other scripts (or a new one) listen for.

            Vector3 playerPos = CharacterComponents.instance.transform.position;
            Vector3 triggerSize = trigger.GetComponent<BoxCollider>().size;

            // #2 Calculate Glow & Distance
            float distanceH = Vector2.Distance(new Vector2(playerPos.x, playerPos.z), new Vector2(trigger.transform.position.x, trigger.transform.position.z));
            glow = CalculateIntensity(distanceH, radius, Mathf.Max(triggerSize.x, triggerSize.z));

            // #3 Calculate Angle Difference and Vignette
            angleDifference = Quaternion.Angle(Quaternion.Euler(CharacterComponents.instance.controller.GetPlayerDirection().x, CharacterComponents.instance.controller.GetPlayerDirection().y, 0), Quaternion.Euler(rot));

            if(distanceH <= radius + 0.2f) //Added some tolerance
                pulse = CalculateIntensity(angleDifference, tolerance_rot, vig_rot);

            //if within radius
            if (distanceH <= radius && CharacterComponents.instance.heldPhoto.IsInFocus())
            {
                vignette = CalculateIntensity(angleDifference, tolerance_rot, vig_rot) * vignetteScalar;

                // #4 Change state if successfully aligned
                if (angleDifference < tolerance_rot)
                {
                    success = true;
                }
            }
        }


        return new StationInfo(success, glow, vignette, pulse);
    }

    private float CalculateIntensity(float value, float min, float max)
    {
        float f = 0f;

        if (value < max)
        {
            if (value >= min)
            {
                f = (value - min) / (max - min);
                f = glow_Curve.Evaluate(f);
            }
            else
            {
                f = 1f;
            }
        }

        return f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(trigger.transform.position, radius);
    }
}
