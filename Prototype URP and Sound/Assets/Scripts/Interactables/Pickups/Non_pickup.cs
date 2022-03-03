using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Non_pickup : MonoBehaviour, IInteractable
{
    public void Interaction()
    {
        if (!GetComponent<Animation>())
            return;

        if (!GetComponent<Animation>().isPlaying)
            GetComponent<Animation>().Play();
    }
}
