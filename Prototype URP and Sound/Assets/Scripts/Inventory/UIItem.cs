using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Known bugs:
//cannot revert back to inventory :(


public class UIItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    private Image spriteImage;
    public UIItem selectedItem;
    public GameObject thoughtsText;
    public GameObject inventoryPanel;
    public GameObject thoughtsPanel;
    public bool examining = false;


    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        thoughtsText = GameObject.Find("ThoughtsText");
        inventoryPanel = GameObject.Find("InventoryPanel");
        thoughtsPanel = GameObject.Find("ThoughtsPanel");
    }

    public void Start()
    {
        thoughtsPanel.gameObject.SetActive(false);
    }
    //Change the item's slot item to match the underlying inventory
    //I think this is where it breaks ??
    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {            
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        else 
        {
            spriteImage.color = Color.clear;
        }
    }

    //handle recognition of clicks on an item, and "select" an item (doesn't work either)
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        examining = true;
        //Debug.Log(this.item.title);
        if (this.item != null)
        {
            thoughtsPanel.gameObject.SetActive(true);
            selectedItem.UpdateItem(this.item);                   
            thoughtsText.GetComponent<Text>().text = selectedItem.item.info["Description"];
            inventoryPanel.gameObject.SetActive(false);
        }
        else if (selectedItem.item != null)
        {          
            selectedItem.UpdateItem(null);
        }
    }
}
