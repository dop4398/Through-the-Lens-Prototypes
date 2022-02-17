using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadBaseEnvironment : MonoBehaviour
{
    private bool collision; //variable to make sure player is in trigger box
    public GameObject toEnableEnvironment;
    public GameObject disableApartment;

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

    // Update is called once per frame
    //on (interact key), set environment active and disable intended apartment
    void Update()
    {
        //if in trigger box AND pressing correct key, do this
        if (collision && PlayerInput.playerInput.interact)
        {
            EnterRoom();
        }
    }

    async void EnterRoom()
    {
        float end = Time.time + Transitioner.instance.transitionTime + 0.5f;
        Transitioner.instance.DoRoomTransition();
        CharacterComponents.instance.controller.LockInput(2f);

        while (Time.time < end)
        {
            await Task.Yield();
        }

        toEnableEnvironment.gameObject.SetActive(true);
        disableApartment.gameObject.SetActive(false);
        CharacterComponents.instance.transform.position = new Vector3(-9.28f, 2.749f, 5.457f);
    }
}
