using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Class <c>FPController</c>.
/// A first-person controller for the player that deals with movement and the camera.
/// </summary>
/// <author>
/// David Patch & Alfie Luo
/// </author>
public class FPController : MonoBehaviour
{
    #region Fields
    //public static FPController instance;
    public GameObject HandWithPhoto;

    public float walkingSpeed = 6.0f;
    public float runningSpeed = 10.0f;
    public bool canJump = false;
    public bool canMove = true;
    public bool canLook = true;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;


    public CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    public float rotationX = 0;
    bool inventoryOn = false;

    //[HideInInspector] public bool canMove = true; // part of old Movement() method
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }


    void Update()
    {
        if (PlayerInput.playerInput.esc && GameManager.instance.State != GameState.Mainmenu)
        {
            GameManager.instance.UpdateGameState(GameState.Pausemenu);
        }

        if (CharacterComponents.instance.playerstate.GetState() == PlayerState.normal)
        {
            if (characterController.enabled)
            {
                Move(PlayerInput.playerInput.input, PlayerInput.playerInput.run, PlayerInput.playerInput.jump);
            }

            Look(PlayerInput.playerInput.mouseLook);

            if (PlayerInput.playerInput.inventory)
            {
                GameManager.instance.UpdateGameState(GameState.Inventory);
            }
            if (PlayerInput.playerInput.album)
            {
                GameManager.instance.UpdateGameState(GameState.Album);
            }
        }
        else if (CharacterComponents.instance.playerstate.GetState() == PlayerState.ui)
        {
            if (PlayerInput.playerInput.inventory)
            {
                GameManager.instance.UpdateGameState(GameState.Game);
            }

            if (PlayerInput.playerInput.album)
            {
                GameManager.instance.UpdateGameState(GameState.Game);
            }
        }
    }


    #region Helper Methods
    public void Move(Vector2 input, bool isRunning, bool isJumping)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * input.y;
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * input.x;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Deal with jumping
        if (canJump && isJumping && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
            if (characterController.isGrounded)
            {
                moveDirection.y = 0f;
            }
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void Move(Vector3 v3)
    {
        //position += v2 * Time.deltaTime;
        //characterController.Move(v2.x * transform.right + v2.z * transform.forward);
        characterController.Move(v3 * Time.deltaTime);
    }

    public void Look(Vector2 mouseLook)
    {
        // Player and Camera rotation
        rotationX += -mouseLook.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseLook.x * lookSpeed, 0);
    }

    public async void LockCameraAndInput(Vector3 euler, float duration)
    {
        float end = Time.time + duration;
        PlayerInput.playerInput.isDisabled = true;
        //float y = transform.rotation.eulerAngles.y;
        //float x = playerCamera.transform.localRotation.eulerAngles.x;

        Debug.Log(euler);
        transform.DORotate(new Vector3(0, euler.y, 0), duration / 2f);
        if(euler.x > 180f)
        {
            rotationX = euler.x - 360f;
        }
        else
        {
            rotationX = Mathf.Abs(euler.x);
        }
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.DOLocalRotate(new Vector3(euler.x, 0, 0), duration / 2f);

        while (Time.time < end)
        {
            await Task.Yield();
        }

        PlayerInput.playerInput.isDisabled = false;
        CharacterComponents.instance.heldPhoto.UnfocusPhoto();

        if (characterController.enabled == false) //resolve a conflict with lerp focus
        {
            characterController.enabled = true;
        }
    }

    public async void LockInput(float duration)
    {
        float end = Time.time + duration;

        PlayerInput.playerInput.isDisabled = true;

        while (Time.time < end)
        {
            await Task.Yield();
        }

        PlayerInput.playerInput.isDisabled = false;
    }

    public Vector3 GetPlayerDirection()
    {
        return new Vector3(playerCamera.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }

    public void SetPlayerDirection(Vector3 dir)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, dir.y, 0));
        playerCamera.transform.rotation = Quaternion.Euler(new Vector3(dir.x, 0, 0));
    }



    /// <summary>
    /// Deals with inputs to let the player look and move around the scene. To be called every frame in Update().
    /// </summary>
    //private void Movement()
    //{
    //    // We are grounded, so recalculate move direction based on axes
    //    Vector3 forward = transform.TransformDirection(Vector3.forward);
    //    Vector3 right = transform.TransformDirection(Vector3.right);
    //    // Press Left Shift to run
    //    bool isRunning = Input.GetKey(KeyCode.LeftShift);
    //    float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
    //    float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
    //    float movementDirectionY = moveDirection.y;
    //    moveDirection = (forward * curSpeedX) + (right * curSpeedY);

    //    if (canJump && Input.GetButton("Jump") && canMove && characterController.isGrounded)
    //    {
    //        moveDirection.y = jumpSpeed;
    //    }
    //    else
    //    {
    //        moveDirection.y = movementDirectionY;
    //    }

    //    // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
    //    // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
    //    // as an acceleration (ms^-2)
    //    if (!characterController.isGrounded)
    //    {
    //        moveDirection.y -= gravity * Time.deltaTime;
    //    }

    //    // Move the controller
    //    characterController.Move(moveDirection * Time.deltaTime);

    //    // Player and Camera rotation
    //    if (canMove)
    //    {
    //        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
    //        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
    //        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    //        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    //    }
    //}
    #endregion
}
