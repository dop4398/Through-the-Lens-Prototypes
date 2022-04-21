using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhotoPickup : Interactable, IInteractable
{
    public GameObject playerPhotoObject;
    public SubScripts subtitles;
    public int index;

    void Start()
    {
        type = InteractableType.Pickup;
    }

    void Update()
    {
        
    }

    public void Interaction()
    {
        // Make the held photo's mesh renderer active
        playerPhotoObject.GetComponent<MeshRenderer>().enabled = true;

        // Enable use of album


        // Make this object inactive
        gameObject.SetActive(false);

        // proc subtitles
        subtitles.tutorialRun(index);
    }
}
