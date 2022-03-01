using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For use on any item around the scene that can be picked up by the player.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class ItemPickup : PickUp, IInteractable
{
    #region Fields
    public ItemDatabase itemData;
    //public float radius { get; set; }
    public string name;
    public int ID; // unique identifier for each item
    #endregion

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    #region Helper Methods
    /// <summary>
    /// Put it in the bag.
    /// </summary>
    public void Interaction()
    {
        base.Interaction();
        // Add the item to the inventory
        CharacterComponents.instance.controller.GetComponent<CollectableInventory>().GiveItem(ID);
        // Set inactive in the scene
        //this.gameObject.SetActive(false);
    }
    #endregion
}
