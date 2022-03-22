using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject ui_inventory;
    public GameObject ui_thoughts;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ui_inventory = GameObject.Find("InventoryPanel");
        ui_thoughts = GameObject.Find("ThoughtsPanel");
    }

    public void TogglePanel()
    {
        if (ui_inventory.activeSelf)
        {
            ui_inventory.SetActive(false);
            ui_thoughts.SetActive(true);
        }
        else
        {
            ui_inventory.SetActive(true);
            ui_thoughts.SetActive(false);
        }
    }
}
