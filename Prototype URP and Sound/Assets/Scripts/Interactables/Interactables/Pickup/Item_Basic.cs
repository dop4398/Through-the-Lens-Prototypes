using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Basic : PickUp
{
    #region Fields
    public Item itemData;
    public int ID; // unique identifier for each item
    #endregion

    void Start()
    {
        base.Start();
    }

    #region Helper Methods
    /// <summary>
    /// Put it in the bag.
    /// </summary>
    public override void Interaction()
    {
        base.Interaction();
        // Add the item to the inventory
        CharacterComponents.instance.controller.GetComponent<CollectableInventory>().GiveItem(ID);
    }

    public override void Use()
    {
        if (!usable)
            return;

        if (!GetComponent<Animation>().isPlaying)
            GetComponent<Animation>().Play();
    }
    #endregion
}
