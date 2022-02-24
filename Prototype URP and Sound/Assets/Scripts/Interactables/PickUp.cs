using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction()
    {
        CharacterComponents.instance.playerstate.SetState(PlayerState.inspecting);
        Inspector.instance.loader.LoadObject(gameObject);
        EventSystem.instance.ItemInspection();
    }
}
