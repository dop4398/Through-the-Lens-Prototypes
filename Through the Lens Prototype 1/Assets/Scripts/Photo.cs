using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple object to store an index that is paired with a Locus.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class Photo : MonoBehaviour
{
    [SerializeField] private string locusID = "";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public string GetID()
    {
        return locusID;
    }
}
