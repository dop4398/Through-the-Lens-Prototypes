using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StationInfo
{
    public bool inRange;
    public float glow;
    public float vignette;

    public StationInfo(bool _inRange, float _glow, float _vignette)
    {
        inRange = _inRange;
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
    public GameObject trigger;

    [Header("Rotation")]
    //correct rotation
    public Vector3 rot;

    [Header("Tolerance")]
    [SerializeField]
    [Range(0.1f, 15.0f)]
    private float tolerance_rot = 15.0f;
    private float angleDifference;

    [Header("Hint - Glow")]
    [SerializeField]
    [Range(15f, 40f)]
    private float glow_rot = 25.0f;

    [Header("Hint - Vignette")]
    [SerializeField]
    [Range(15f, 40f)]
    private float vig_rot = 25.0f;

    [Header("Other")]
    public State state; //current state
    public bool isActive; //control if it needs to check player's status
    public bool isSingleUse; //destory component after a toggle
    public KeyCode key; //debug key


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
            Destroy(GetComponent<Locus>());
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

        //if locus is active, check player status
        if (isActive)
        {
            if (FPController.instance.GetHeldPhotoID() == id && FPController.instance.IsInFocus())
            {
                angleDifference = Quaternion.Angle(Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, FPController.instance.transform.rotation.eulerAngles.y, 0), Quaternion.Euler(rot));

                glow = CalculateIntensity(angleDifference, tolerance_rot, glow_rot);

                vignette = CalculateIntensity(angleDifference, tolerance_rot, vig_rot);

                if (angleDifference < 1f + tolerance_rot)
                {
                    success = true;

                    //swap photo content
                    FPController.instance.SwapPhotoContent();
                }
            }
        }

        return new StationInfo(success, glow, vignette);
    }

    private float CalculateIntensity(float rot, float min, float max)
    {
        float f = 0f;

        if (rot < max)
        {
            if (rot >= min)
            {
                f = (rot - min) / (max - min);
                f = 1f - f;
            }
            else
            {
                f = 1f;
            }
        }

        return f;
    }
}
