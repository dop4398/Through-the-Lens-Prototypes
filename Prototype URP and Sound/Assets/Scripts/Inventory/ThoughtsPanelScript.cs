using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThoughtsPanelScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject inventoryPanel;



    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        inventoryPanel.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
