using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For use on any regular door to open and close.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class Door : MonoBehaviour, IInteractable
{
    #region Fields
    public float radius { get; set; }
    public bool requiresKey;
    #endregion


    void Start()
    {
        radius = 2.0f;
    }

    void Update()
    {

    }

    #region Helper Methods
    public void OnMouseOver()
    {

    }

    public void OnMouseExit()
    {

    }

    public void Interaction()
    {

    }
    #endregion
}
