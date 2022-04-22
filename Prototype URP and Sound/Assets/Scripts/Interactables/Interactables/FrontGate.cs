using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontGate : Interactable, IInteractable
{
    #region Fields
    //public FMOD.Studio.EventInstance interactSound;
    public FMODUnity.StudioEventEmitter emitter;
    public int subtitleIndex = 6;
    public SubScripts subtitles;
    #endregion

    void Start()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        type = InteractableType.NonPickup;
    }


    void Update()
    {

    }


    public void Interaction()
    {
        emitter.Play();
        subtitles.tutorialRun(subtitleIndex);
    }
}
