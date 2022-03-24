using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSFX : Interactable, IInteractable
{
    #region Fields
    //public FMOD.Studio.EventInstance interactSound;
    public FMODUnity.StudioEventEmitter emitter;
    #endregion

    void Start()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        type = InteractableType.Examinable;
    }

    
    void Update()
    {
        
    }


    public void Interaction()
    {
        emitter.Play();
    }
}
