using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void Awake()
    {
        BuildDatabase();
    }

    //access item based on ID
    public Item GetItem(int id)
    {
        return items.Find(item => item.ID == id);
    }
    //access item based on name
    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    //Would this be a list of "EVERYTHING" the player can access? or something dynamic based on the items in front of them?
    //I think the former but I'm not sure
    //Current Items are for testing purposes
    void BuildDatabase()
    {
        items = new List<Item> {
                new Item(0, "sadcowboy", "A cowboy",
                new Dictionary<string, string>
                {
                    { "Name", "Relic 1" },
                    { "Description", "The young cowboy emoji seems very sad"}
                }, "Collectable"),
                new Item(1, "remorseful_cowboy", "A cowboy",
                new Dictionary<string, string>
                {
                    { "Name", "Relic 2" },
                    { "Description", "Oh my god he has a gun"}
                }, "Photo"),
                new Item(2, "upsidedown_cowboy", "A cowboy",
                new Dictionary<string, string>
                {
                    { "Name", "Relic 3" },
                    { "Description", "What's wrong buddy?"}
                }, "Relic"),
                new Item(3, "AmongUs", "A cowboy",
                new Dictionary<string, string>
                {
                    { "Name", "Relic 3" },
                    { "Description", "A strange spaceman figurine. Perhaps it was a toy of one of the children?"}
                }, "Collectable"),
            };
    }
}
