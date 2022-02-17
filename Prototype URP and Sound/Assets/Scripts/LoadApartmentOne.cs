using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadApartmentOne : MonoBehaviour
{
    private bool collision; //variable to make sure player is in trigger box
    public GameObject toEnableApartment;
    public GameObject disableEnvironment;
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
    //on (interact key), set apartment active and disable outer environment
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

        while(Time.time < end)
        {
            await Task.Yield();
        }

        toEnableApartment.gameObject.SetActive(true);
        disableEnvironment.gameObject.SetActive(false);
        playerChar.transform.position = new Vector3(-11.42f, 2.992f, 5.592f);
    }
}
