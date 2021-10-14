using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Locus</c>.
/// Used to check and react to player's action and transform, and control objects, materials, and probably many other things to change based on the state of the Locus.
/// </summary>
/// <author>
/// Alfie Luo
/// </author>
public class Locus : MonoBehaviour
{

    //Locus's state is a enum, this is for better script readabily and potential future expansion of the locus class.
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

    [Header("Rotation")]
    //correct position and rotation
    private Vector3 pos;
    public Vector3 rot;

    //used to specify the 'range' of the currect position and rotation to make pos and rot matching easier
    private float tolerance_pos;
    [Header("Tolerance")]
    [SerializeField]
    [Range(0.1f, 15.0f)]
    private float tolerance_rot = 15.0f;

    [Header("Other")]
    public State state; //current state
    public bool isActive; //control if it needs to check player's status
    public bool isSingleUse; //destory component after a toggle
    public KeyCode key; //debug key


    //private GameObject player; //Player Reference
    [Header("CD")]
    public float cooldownDuration = 2.0f;
    [SerializeField] private float cooldown = 0.0f;


    void Start()
    {
        Init();
    }


    void Update()
    {
        if (Input.GetKeyDown(key))
            ToggleState();

        CheckPlayer();
    }

    /// <summary>
    /// Class initializaiton
    /// </summary>
    private void Init()
    {
        pos = this.pos;
        rot = this.rot;
        state = this.state;
        isSingleUse = this.isSingleUse;
    }

    /// <summary>
    /// Check if player is holding the correct photo and if player's current rotation and position is correct. If correct, toggle state.
    /// </summary>
    private void CheckPlayer()
    {
        //if locus is active, check player status
        if (isActive && cooldown <= 0.0f)
        {
            if(FPController.instance.GetHeldPhotoID() == id && FPController.instance.IsInFocus())
            {
                cooldown = cooldownDuration;
                if (Quaternion.Angle(Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, FPController.instance.transform.rotation.eulerAngles.y, 0), Quaternion.Euler(rot)) < 1f + tolerance_rot)
                {
                    ToggleState();
                }
            }         
        }

        // tick down the cooldown timer, or reset it if 0 is reached
        if(cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Helper function, Toggle the state of the current locus between past and present, this method exists be cause locus only has two states.
    /// </summary>
    private void ToggleState()
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

    private void OnTriggerEnter(Collider other)
    {
        isActive = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
    }
}
