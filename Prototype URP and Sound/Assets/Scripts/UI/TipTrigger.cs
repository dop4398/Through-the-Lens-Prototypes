using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipTrigger : MonoBehaviour
{

    public string name;
    public float duration;
    public bool removeOnExit;
    public bool triggerOnce;
    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && flag)
        {

            Debug.Log("Triggered");

            if (duration > 0)
                TipSystem.instance.ShowTip(name, duration);
            else
                TipSystem.instance.ShowTip(name);

            if (triggerOnce)
                flag = !flag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (removeOnExit)
                TipSystem.instance.RemoveTip(name);
        }
    }
}
