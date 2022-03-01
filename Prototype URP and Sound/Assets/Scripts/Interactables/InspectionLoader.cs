using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InspectionLoader : MonoBehaviour
{
    private Vector3 position;
    [SerializeField]
    private float transitionTime;
    private GameObject item;

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
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero - new Vector3(0f, 2f, 0f);
        obj.transform.DOLocalMove(Vector3.zero, transitionTime).OnComplete(() => { Inspector.instance.controller.enabled = true; });
    }
}
