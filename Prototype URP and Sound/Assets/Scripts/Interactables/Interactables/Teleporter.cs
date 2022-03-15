using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Teleporter : Interactable, IInteractable
{
    public Transform transform_enter;
    public Transform transform_exit;

    public List<GameObject> obj_enter;
    public List<GameObject> obj_exit;

    //public bool requireKey = false;
    public int key_id;
    public bool isLocked = true;

    public float transitionTime;

    private bool active;
    private bool flag = true;

    private void Start()
    {
        type = InteractableType.Teleporter;
    }

    public void Interaction()
    {
        if (isLocked)
        {
            if (CharacterComponents.instance.inventory.CheckForItem(key_id) != null)
            {
                isLocked = false;
                Teleport();
            }
            else
            {
                LockedBehavior();
            }
        }
        else
        {
            Teleport();
        }
    }

    async void Teleport()
    {
        Debug.Log("Teleportation happenning");
        float end = Time.time + Transitioner.instance.transitionTime + 0.5f;
        Transitioner.instance.DoRoomTransition();
        CharacterComponents.instance.controller.LockInput(2f);

        while (Time.time < end)
        {
            await Task.Yield();
        }

        ToggleObjects();
        MovePlayer();

        flag = !flag;
    }

    void ToggleObjects()
    {
        if (flag)
        {
            if (obj_enter.Count > 0)
            {
                foreach (GameObject g in obj_enter)
                {
                    g.SetActive(true);
                }
            }
            if (obj_exit.Count > 0)
            {
                foreach (GameObject g in obj_exit)
                {
                    g.SetActive(false);
                }
            }
        }
        else
        {
            if (obj_enter.Count > 0)
            {
                foreach (GameObject g in obj_enter)
                {
                    g.SetActive(false);
                }
            }
            if (obj_exit.Count > 0)
            {
                foreach (GameObject g in obj_exit)
                {
                    g.SetActive(true);
                }
            }
        }
    }

    void MovePlayer()
    {
        if (flag)
        {
            CharacterComponents.instance.controller.characterController.enabled = false;
            CharacterComponents.instance.gameObject.transform.position = transform_enter.position;
            CharacterComponents.instance.controller.characterController.enabled = true;
        }
        else
        {
            CharacterComponents.instance.controller.characterController.enabled = false;
            CharacterComponents.instance.gameObject.transform.position = transform_exit.position;
            CharacterComponents.instance.controller.characterController.enabled = true;
        }
    }

    void LockedBehavior()
    {
        Debug.Log("Door's locked");
    }

    public bool CanUnlock()
    {
        return CharacterComponents.instance.inventory.CheckForItem(key_id) != null && isLocked;
    }
}
