using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTrigger : MonoBehaviour
{
    private Station owner;

    // Start is called before the first frame update
    void Start()
    {
        owner = transform.parent.GetComponent<Station>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        StationManager.instance.AddStation(owner);
    }

    private void OnTriggerExit(Collider other)
    {
        StationManager.instance.RemoveStation(owner);
    }
}
