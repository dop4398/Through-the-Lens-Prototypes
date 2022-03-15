using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        item = obj;
        item.transform.parent = transform;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        item.transform.rotation = Quaternion.Euler(Vector3.zero);
        item.transform.localPosition = Vector3.zero - new Vector3(0f, 2f, 0f);
        item.transform.DOLocalMove(Vector3.zero, transitionTime).OnComplete(() => { Inspector.instance.controller.enabled = true; });
        PPVController.instance.SetDoF(true);
    }
}
