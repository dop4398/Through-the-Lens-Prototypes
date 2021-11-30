using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HeldPhoto is the core photo mechanic.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class HeldPhoto : MonoBehaviour
{
    #region Fields
    public GameObject photoObject;
    [SerializeField] private bool photoInFocus = false;
    #endregion
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    #region Helper Methods
    /// <summary>
    /// Bring the currently held photo into focus in the center of the screen, blocking a good section of the player's vision.
    /// </summary>
    public void FocusPhoto()
    {
        photoObject.transform.localPosition = new Vector3(0, 0, 0.5f);
        photoInFocus = true;
    }

    /// <summary>
    /// Remove the photo from the center of the screen. Brings it out of focus.
    /// </summary>
    public void UnfocusPhoto()
    {
        photoObject.transform.localPosition = new Vector3(0.75f, -0.4f, 1.5f);
        photoInFocus = false;
    }


    /// <summary>
    /// Public method for checking whether the player has a photo in focus.
    /// </summary>
    /// <returns>True if a photo is in focus, false if not.</returns>
    public bool IsInFocus()
    {
        return photoInFocus;
    }
    #endregion
}
