using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Code Reference: https://answers.unity.com/questions/177391/drag-to-rotate-gameobject.html
/// </summary>
public class InspectionControl : MonoBehaviour
{
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        if(CharacterComponents.instance.playerstate.GetState() == PlayerState.inspecting)
        {
            if (PlayerInput.playerInput.interact)
            {
                // rotating flag
                _isRotating = true;

                // store mouse
                _mouseReference = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E))
            {
                // rotating flag
                _isRotating = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetRotation();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                PPVController.instance.SetDoF(false);
                ExitInspection();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Inspector.instance.loader.item.GetComponent<PickUp>().Use();
                Debug.Log(Inspector.instance.loader.item.GetComponent<PickUp>());
            }

            if (_isRotating)
            {
                // offset
                _mouseOffset = (Input.mousePosition - _mouseReference);

                // apply rotation
                _mouseOffset.y = -_mouseOffset.y * _sensitivity;
                _mouseOffset.x = -_mouseOffset.x * _sensitivity;

                // rotate
                transform.Rotate(Vector3.up, _mouseOffset.x, Space.World);
                transform.Rotate(Vector3.right, _mouseOffset.y, Space.World);

                // store mouse
                _mouseReference = Input.mousePosition;
            }
        }
    }

    private void ResetRotation()
    {
        transform.DORotate(Vector3.zero, 0.5f);
        //transform.DOLocalRotate(Vector3.zero, 0.5f);
    }

    private void ExitInspection()
    {
        CharacterComponents.instance.playerstate.SetState(PlayerState.normal);
        EventSystem.instance.ItemInspectionExit();
        this.enabled = false;
    }
}
