using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBaseEnvironment : MonoBehaviour
{
    private bool collision; //variable to make sure player is in trigger box
    public GameObject toEnableEnvironment;
    public GameObject disableApartment;
    public GameObject playerChar;

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
        if (collision && Input.GetKeyDown(KeyCode.E))
        {
            toEnableEnvironment.gameObject.SetActive(true);
            disableApartment.gameObject.SetActive(false);
            playerChar.transform.position = new Vector3(-9.28f, 2.749f, 5.457f);
        }
    }
}
