using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{

    public InteractableType type = InteractableType.Teleporter;

    public Transform transform_enter;
    public Transform transform_exit;

    public List<GameObject> obj_enter;
    public List<GameObject> obj_exit;

    public bool requireKey = false;
    public int key_id;

    public float transitionTime;

    private bool active;
    private bool flag = true;

    public void Interaction()
    {
        if (requireKey)
        {
            if (CharacterComponents.instance.inventory.CheckForItem(key_id) != null)
            {
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
            CharacterComponents.instance.gameObject.transform.position = transform_enter.position;
        }
        else
        {
            CharacterComponents.instance.gameObject.transform.position = transform_exit.position;
        }
    }

    void LockedBehavior()
    {
        Debug.Log("Door's locked");
    }
}
