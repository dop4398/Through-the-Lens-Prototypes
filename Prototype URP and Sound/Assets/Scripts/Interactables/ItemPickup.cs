using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For use on any item around the scene that can be picked up by the player.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class ItemPickup : MonoBehaviour, IInteractable
{
    #region Fields

    public ItemDatabase itemData;

    public float radius { get; set; }
    public string name;
    public int ID; // unique identifier for each item
    #endregion

    void Start()
    {
        radius = 10.0f;

        

    }

    void Update()
    {
        
    }

    #region Helper Methods
    public void OnMouseOver()
    {
        if (Vector3.Distance(CharacterComponents.instance.controller.transform.position, this.gameObject.transform.position) <= radius)
        {
            // Call UI method here
            Debug.Log("Close enough :)");

            if(PlayerInput.playerInput.interact)
            {
                Interaction();
            }
        }
    }

    public void OnMouseExit()
    {
        // Turn off UI popup here
    }

    /// <summary>
    /// Put it in the bag.
    /// </summary>
    public void Interaction()
    {
        // Add the item to the inventory
        CharacterComponents.instance.controller.GetComponent<CollectableInventory>().GiveItem(ID);
        // Set inactive in the scene
        this.gameObject.SetActive(false);
    }
    #endregion
}
