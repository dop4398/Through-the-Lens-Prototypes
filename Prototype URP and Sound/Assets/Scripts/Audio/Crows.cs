using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Crows : MonoBehaviour
{
    public GameObject[] crows;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject c in crows)
        {
            c.GetComponent<StudioEventEmitter>().SetParameter("PlayerIsOutside", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
