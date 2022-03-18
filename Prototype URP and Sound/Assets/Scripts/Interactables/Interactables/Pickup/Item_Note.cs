using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Note : PickUp
{
    #region Fields
    public Item itemData;
    public int ID; // unique identifier for each item
    public TextAsset text;
    private bool flag = true;
    #endregion

    void Start()
    {
        base.Start();
    }

    #region Helper Methods
    /// <summary>
    /// Put it in the bag.
    /// </summary>
    override public void Interaction()
    {
        base.Interaction();
        // Add the item to the inventory
        CharacterComponents.instance.controller.GetComponent<CollectableInventory>().GiveItem(ID);
    }

    public override void Use()
    {
        if (!usable)
            return;
        if (flag)
        {
            NoteDisplay.instance.ShowNote(text);
        }
        else
        {
            NoteDisplay.instance.Disable();
        }

        flag = !flag;
    }
    #endregion
}
