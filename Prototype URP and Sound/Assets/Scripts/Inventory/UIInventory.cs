using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Known bugs:
//None

public class UIInventory : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public GameObject thoughtsPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 4;
    public int numOfSlotsUsed = 0;
    public bool isShowing = false;
  

    private void Awake()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    //Update the visual slot item with the image of the piece given to it, this allows us to show the player exactly what they have
    public void UpdateSlot(int slot, Item item)
    {        
        uIItems[slot].UpdateItem(item);
    }


    //Increment the items in the list, and keep track of how many slots are used (in case of updating the visuals to only have the desired slots shown)
    public void AddNewItem(Item item)
    {
        UpdateSlot(numOfSlotsUsed, item);
        numOfSlotsUsed++;
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }

    public void Examine(Item item)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
         
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("Hover");
        //throw new System.NotImplementedException();
    }
}
