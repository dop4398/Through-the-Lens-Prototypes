using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinable : MonoBehaviour, IInteractable
{
    public int id;

    public void Interaction()
    {
        SubScripts.instance.Run(id);
    }
}
