using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
//Item Base class, could be upgraded into an abstract class that parents photos, relics, and general consumables
public class Item
{
    //THOUGHTS:
    //Enums for collectable, relic, photo?
    //Collectable and relic are handled similarly but within the inventory they look different
    //Photos are placed in a separate list and accessed differently
    //Maybe child classes???

    public enum Type {collectable, relic, photo, notes};
    public Type myType;
    public int ID;
    public string title;
    public Sprite icon;
    public string info;
    public GameObject prefab;

    public Item(int id, string title, string description, string info, string type)
    {
        this.ID = id;
        this.title = title;
        this.icon = Resources.Load<Sprite>("Sprites/InventoryTestSprites/" + title);
        this.info = info;
        if (type == "photo")
        {
            this.myType = Type.photo;
        }
        else if (type == "relic")
        {
            this.myType = Type.relic;
        }
        else
        {
            this.myType = Type.collectable;
        }
              
    }

    public Item(Item item)
    {
        this.ID = item.ID;
        this.title = item.title;
        this.icon = Resources.Load<Sprite>("Sprites/InventoryTestSprites/" + title);
        this.info = item.info;
        this.myType = Type.collectable;
    }
}
