using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>FPController</c>.
/// A first-person controller for the player. It deals with movement and the camera.
/// This script was originally sourced from <a href="https://sharpcoderblog.com/blog/unity-3d-fps-controller">here</a>.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class FPController : MonoBehaviour
{
    #region fields
    public static FPController instance;

    public float walkingSpeed = 6.0f;
    public float runningSpeed = 10.0f;
    public bool canJump = false;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector] public bool canMove = true;

    // To be removed once the album is implemented
    public List<Photo> album;
    [SerializeField] private Photo currentPhoto;
    private int albumIndex = 0;

    //Hard-coded item stuff
    public GameObject photoObject;
    [SerializeField] private bool photoInFocus = false;
    public bool hasKey = false;
    #endregion

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        Movement();
        FocusPhoto();

        //Debug.Log(GetHeldPhotoIndex());

        if (Input.GetKeyDown(KeyCode.E))
        {
            CyclePhoto();
        }
    }


    #region helper methods
    /// <summary>
    /// Deals with inputs to let the player look and move around the scene. To be called every frame in Update().
    /// </summary>
    private void Movement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (canJump && Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    /// <summary>
    /// Method for retrieving the Photo that the player is holding, if any.
    /// </summary>
    /// <returns>The index of the Photo being held by the player, or null if none.</returns>
    public string GetHeldPhotoID()
    {
        return currentPhoto.GetID();
    }

    /// <summary>
    /// Changes the currently held photo by cycling through the list of all the player's photos (their album).
    /// This is a quick and dirty solution and should be improved upon for future prototypes.
    /// </summary>
    public void CyclePhoto()
    {
        albumIndex++;
        if(albumIndex >= album.Count)
        {
            albumIndex = 0;
        }

        currentPhoto = album[albumIndex];
        Debug.Log(currentPhoto.GetID() + " - " + albumIndex);

        photoObject.GetComponent<MeshRenderer>().material = currentPhoto.GetMaterial_Current();
    }

    /// <summary>
    /// Bring the currently held photo into focus in the center of the screen, blocking a good section of the player's vision.
    /// </summary>
    public void FocusPhoto()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            photoObject.transform.localPosition = new Vector3(0, 0, 0.5f);
            photoInFocus = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            photoObject.transform.localPosition = new Vector3(0.75f, -0.5f, 1.5f);
            photoInFocus = false;
        }
    }

    /// <summary>
    /// Public method for checking whether the player has a photo in focus.
    /// </summary>
    /// <returns>True if a photo is in focus, false if not.</returns>
    public bool IsInFocus()
    {
        return photoInFocus;
    }

    /// <summary>
    /// Toggle the state of the current held photo and update the material of the photo object
    /// </summary>
    public void SwapPhotoContent()
    {
        currentPhoto.ToggleState();
        photoObject.GetComponent<MeshRenderer>().material = currentPhoto.GetMaterial_Current();
    }
    #endregion
}
