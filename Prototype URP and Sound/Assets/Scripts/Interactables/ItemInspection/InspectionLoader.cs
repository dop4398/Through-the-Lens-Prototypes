using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InspectionLoader : MonoBehaviour
{
    [SerializeField]
    private float transitionTime;
    public GameObject item = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadObject(GameObject obj)
    {
        if (obj == null)
            return;

        if (item != null)
        {
            Destroy(item);
        }

        EventSystem.instance.ItemInspection();

        item = obj;

        item.transform.parent = transform;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        item.transform.rotation = Quaternion.Euler(Vector3.zero);
        item.transform.localPosition = Vector3.zero - new Vector3(0f, 2f, 0f);

        if (item.GetComponent<Item_Basic>() != null)
        {
            Inventory.instance.ui_thoughts.GetComponentInChildren<Text>().text = item.GetComponent<Item_Basic>().itemData.info;
            Debug.Log(item.GetComponent<Item_Basic>().itemData.info);
        }
        else
        {
            Inventory.instance.ui_thoughts.GetComponentInChildren<Text>().text = "";
        }

        item.transform.DOLocalMove(Vector3.zero, transitionTime).OnComplete(() => { 
            Inspector.instance.controller.enabled = true; 
            item.GetComponent<PickUp>().RemoveHighlight(); 
        });
        PPVController.instance.SetDoF(true);
    }
}
