using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInventory : MonoBehaviour
{

    //appropriate lists
    public List<Item> collectables = new List<Item>();
    public List<Item> relics = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    public GameObject ui;
    //reference to album stuff

    private void Start()
    {
        //GiveItem(0);
        //GiveItem(2);
        ui.SetActive(inventoryUI.isShowing);
    }

    //give the player an Item that they can access and view
    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);

        if (itemToAdd.myType == Item.Type.collectable)
        {
            collectables.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added Item to Collectables: " + itemToAdd.title);
        }
        else if (itemToAdd.myType == Item.Type.relic)
        {
            relics.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added Item to Relics: " + itemToAdd.title);
        }
        //else add to album

        
    }

    public void GiveItem(string name)
    {
        Item itemToAdd = itemDatabase.GetItem(name);
        if (itemToAdd.myType == Item.Type.collectable)
        {
            collectables.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added Item to Collectables: " + itemToAdd.title);
        }
        else if (itemToAdd.myType == Item.Type.relic)
        {
            relics.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added Item to Relics: " + itemToAdd.title);
        }
    }

    public Item CheckForItem(int id)
    {
        return collectables.Find(item => item.ID == id);
    }

    public void RemoveItem(int id)
    {
        Item item = CheckForItem(id);
        if (item != null)
        {
            collectables.Remove(item);
            inventoryUI.RemoveItem(item);
        }
    }

    public void DisplayInventory()
    {
        inventoryUI.isShowing = !inventoryUI.isShowing;
        ui.SetActive(inventoryUI.isShowing);
    }
}
