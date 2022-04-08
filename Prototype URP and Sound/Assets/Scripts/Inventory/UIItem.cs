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
    public bool examining = false;


    private void Awake()
    {

    }

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
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
            Inventory.instance.ui_thoughts.GetComponentInChildren<Text>().text = this.item.info;
            Inspector.instance.loader.LoadObject(Instantiate(item.prefab, new Vector3(0, 1000, 0), Quaternion.identity));
        }
    }
}
