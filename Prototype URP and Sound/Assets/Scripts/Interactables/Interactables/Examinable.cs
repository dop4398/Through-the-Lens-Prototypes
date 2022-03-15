using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinable : Interactable, IInteractable
{
    public int id;

    private void Start()
    {
        type = InteractableType.Examinable;
    }

    public void Interaction()
    {
        SubScripts.instance.Run(id);
    }
}
