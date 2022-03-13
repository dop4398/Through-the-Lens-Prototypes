using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_pickup : Interactable, IInteractable
{
    private void Start()
    {
        type = InteractableType.NonPickup;
    }

    public void Interaction()
    {
        if (!GetComponent<Animation>())
            return;

        if (!GetComponent<Animation>().isPlaying)
            GetComponent<Animation>().Play();
    }
}
