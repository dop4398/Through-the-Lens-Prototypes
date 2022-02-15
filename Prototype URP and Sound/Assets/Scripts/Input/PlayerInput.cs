using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerInput deals with input from the player during regular gameplay. 
/// This script holds a reference to the character and calls its movement functions.
/// </summary>
/// <author>
/// Alfie Luo
/// </author>

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput playerInput;
    public bool isDisabled;

    public Vector2 input
    {
        get
        {
            if (this.isDisabled) return Vector2.zero;

            Vector2 i = Vector2.zero;
            i.x = Input.GetAxis("Horizontal");
            i.y = Input.GetAxis("Vertical");
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public Vector2 raw
    {
        get
        {
            if (this.isDisabled) return Vector2.zero;

            Vector2 i = Vector2.zero;
            i.x = Input.GetAxisRaw("Horizontal");
            i.y = Input.GetAxisRaw("Vertical");
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public Vector2 mouseLook
    {
        get
        {
            if (this.isDisabled) return Vector2.zero;

            Vector2 i = Vector2.zero;
            i.x = Input.GetAxis("Mouse X");
            i.y = Input.GetAxis("Mouse Y");
            return i;
        }
    }

    public bool run
    {
        get { if (this.isDisabled) return false; return Input.GetKey(KeyCode.LeftShift); }
    }

    //public bool sneak
    //{
    //    get { return Input.GetKeyDown(KeyCode.C); }
    //}

    //public bool sneaking
    //{
    //    get { return Input.GetKey(KeyCode.C); }
    //}

    public bool interact
    {
        get { if (this.isDisabled) return false; return Input.GetKeyDown(KeyCode.E); }
    }

    public bool focusPhoto
    {
        get { if (this.isDisabled) return false; return Input.GetMouseButtonDown(1); }
    }

    public bool unfocusPhoto
    {
        get { if (this.isDisabled) return false; return Input.GetMouseButtonUp(1); }
    }

    //public bool aim
    //{
    //    get { return Input.GetMouseButton(1); }
    //}
    public bool inventory
    {
        get { if (this.isDisabled) return false; return Input.GetKeyDown(KeyCode.Q); }
    }
    public bool album
    {
        get { if (this.isDisabled) return false; return Input.GetKeyDown(KeyCode.Tab); }
    }

    public bool swap
    {
        get { if (this.isDisabled) return false; return Input.GetAxis("Mouse ScrollWheel") > 0f; }
    }

    public bool jump
    {
        get { if (this.isDisabled) return false; return Input.GetKeyDown(KeyCode.Space); }
    }

    private void Awake()
    {
        playerInput = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
