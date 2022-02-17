using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase database;

    [SerializeField]
    public List<Item> items = new List<Item>();

    public void Awake()
    {
        database = this;
        //BuildDatabase();
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
}
