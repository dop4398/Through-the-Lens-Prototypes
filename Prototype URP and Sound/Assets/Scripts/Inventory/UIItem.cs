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
    public Image spriteImage;
    public Text thoughtsText;
    public GameObject inventoryPanel;
    public GameObject thoughtsPanel;
    public bool examining = false;


    private void Awake()
    {

    }

    public void Start()
    {

    }

    public void Init()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        thoughtsText = Inventory.instance.ui_thoughts.GetComponentInChildren<Text>();
        inventoryPanel = Inventory.instance.ui_inventory;
        thoughtsPanel = Inventory.instance.ui_thoughts;
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
            spriteImage.preserveAspect = true;
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
            thoughtsText.text = this.item.info;
            CharacterComponents.instance.playerstate.SetState(PlayerState.inspecting);
            Inspector.instance.loader.LoadObject(Instantiate(item.prefab, new Vector3(0, 1000, 0), Quaternion.identity));
        }
    }
}
