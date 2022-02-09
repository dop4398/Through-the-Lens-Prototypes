using System.Collections;
using System.Collections.Generic;
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
    //on backslash press (interact key), set environment active and disable intended apartment
    void Update()
    {
        //if in trigger box AND pressing correct key, do this
        if (collision && Input.GetKeyDown(KeyCode.Backslash))
        {
            toEnableEnvironment.gameObject.SetActive(true);
            disableApartment.gameObject.SetActive(false);
        }
    }
}
