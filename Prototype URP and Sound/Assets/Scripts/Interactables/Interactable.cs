using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface <class>Interactable</class>
/// Script for interactable logic. For use with both pickups and other interactable objects.
/// </summary>
/// <author>
/// David Patch
/// </author>
public interface IInteractable
{
    #region Fields
    //float radius { get; set; }
    #endregion

    void Interaction();
}
