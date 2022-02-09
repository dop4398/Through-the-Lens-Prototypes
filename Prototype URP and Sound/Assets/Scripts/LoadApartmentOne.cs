using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadApartmentOne : MonoBehaviour
{
    private bool collision; //variable to make sure player is in trigger box
    public GameObject toEnableApartment;
    public GameObject disableEnvironment;

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

    // Update is called once per frame
    //on backslash press (interact key), set apartment active and disable outer environment
    void Update()
    {
        //if in trigger box AND pressing correct key, do this
        if (collision && Input.GetKeyDown(KeyCode.Backslash))
        {
            toEnableApartment.gameObject.SetActive(true);
            disableEnvironment.gameObject.SetActive(false);
        }
    }
}
