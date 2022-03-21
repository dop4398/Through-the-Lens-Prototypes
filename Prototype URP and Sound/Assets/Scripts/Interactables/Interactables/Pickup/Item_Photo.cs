using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Photo : PickUp
{
    #region Fields
    //public float radius { get; set; }
    public Photo photo;
    #endregion

    new void Start()
    {
        type = InteractableType.Pickup;
    }

    #region Helper Methods
    /// <summary>
    /// Put it in the bag.
    /// </summary>
    override public void Interaction()
    {
        base.Interaction();
        // Add the item to the album
        CharacterComponents.instance.album.AddPhoto(photo);
    }

    public override void Use()
    {
        base.Use();
    }
    #endregion
}
