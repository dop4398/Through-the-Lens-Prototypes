using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadApartmentOne : MonoBehaviour
{
    private bool collision;

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collision && Input.GetKeyDown(KeyCode.Backslash))
        {
            SceneManager.LoadScene(1);
        }
    }
}
