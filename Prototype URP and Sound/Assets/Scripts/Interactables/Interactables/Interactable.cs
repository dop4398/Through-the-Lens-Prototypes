using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected InteractableType type;

    public InteractableType GetInteractionType()
    {
        return type;
    }
}
