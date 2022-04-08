using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Transitioner transitioner;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        transitioner.GetComponentInChildren<Transitioner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
