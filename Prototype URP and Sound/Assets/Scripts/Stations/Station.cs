using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StationInfo
{
    public bool success;
    public float glow;
    public float vignette;

    public StationInfo(bool _success, float _glow, float _vignette)
    {
        success = _success;
        glow = _glow;
        vignette = _vignette;
    }
}

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

    [Header("Activation Box")]
    public GameObject trigger;

    [Header("Correct Radius")]
    public float radius;

    [Header("Rotation")]
    //correct rotation
    public Vector3 rot;

    [Header("Tolerance")]
    [SerializeField]
    [Range(0.1f, 15.0f)]
    private float tolerance_rot = 15.0f;
    private float angleDifference;

    [Header("Hint - Vignette Angle")]
    [SerializeField]
    [Range(15f, 40f)]
    private float vig_rot = 25.0f;

    [Header("Other")]
    public State state; //current state
    public bool isSingleUse; //destory component after a toggle

    [HideInInspector]
    public KeyCode key; //debug key

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.playerInput.jump)
        {
            ToggleState();
        }
    }

    /// <summary>
    /// Class initializaiton
    /// </summary>
    private void Init()
    {
        rot = this.transform.rotation.eulerAngles;
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
        float glow = 0f;
        float vignette = 0f;

        // #1 Check Photo ID
        if (true)
        {

            Vector3 playerPos = CharacterComponents.instance.transform.position;
            Vector3 triggerSize = trigger.GetComponent<BoxCollider>().size;

            // #2 Calculate Glow & Distance
            float distanceH = Vector2.Distance(new Vector2(playerPos.x, playerPos.z), new Vector2(trigger.transform.position.x, trigger.transform.position.z));
            glow = CalculateIntensity(distanceH, radius, Mathf.Max(triggerSize.x, triggerSize.z));

            //if within radius
            if(distanceH <= radius)
            {
                // #3 Calculate Angle Difference and Vignette
                angleDifference = Quaternion.Angle(Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, CharacterComponents.instance.transform.rotation.eulerAngles.y, 0), Quaternion.Euler(rot));

                vignette = CalculateIntensity(angleDifference, tolerance_rot, vig_rot);

                // #4 Change state if successfully aligned
                if (angleDifference < 1f + tolerance_rot)
                {
                    success = true;
                }
            }
        }


        return new StationInfo(success, glow, vignette);
    }

    private float CalculateIntensity(float value, float min, float max)
    {
        float f = 0f;

        if (value < max)
        {
            if (value >= min)
            {
                f = (value - min) / (max - min);
                f = 1f - f;
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
