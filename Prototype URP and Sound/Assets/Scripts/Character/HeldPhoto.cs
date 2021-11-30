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
    public Photo heldPhoto;
    public int heldPhotoIndex;
    [SerializeField] private bool photoInFocus = false;
    #endregion

    void Start()
    {
        
    }
    
    void Update()
    {
        if(PlayerInput.playerInput.swap)
        {
            CyclePhoto();
        }

        if(PlayerInput.playerInput.focusPhoto)
        {
            FocusPhoto();
        }
        else if(PlayerInput.playerInput.unfocusPhoto)
        {
            UnfocusPhoto();
        }
    }

    #region Helper Methods
    /// <summary>
    /// Sets the held photo of the player to the given parameter.
    /// </summary>
    /// <param name="photo"></param>
    public void SetHeldPhoto(Photo photo)
    {
        heldPhoto = photo;

        // Update the photo object being held
        Toggle the state and whatnot
    }

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

    /// <summary>
    /// Changes the currently held photo by cycling through the list of all the player's photos (their album).
    /// This is a quick and dirty solution and should be improved upon for future prototypes.
    /// </summary>
    public void CyclePhoto()
    {
        heldPhotoIndex++;
        if (heldPhotoIndex >= CharacterComponents.instance.album.GetAlbumSize())
        {
            heldPhotoIndex = 0;
        }

       SetHeldPhoto(CharacterComponents.instance.album.GetPhotoAtIndex(heldPhotoIndex));

        Debug.Log(heldPhoto.GetID() + " - " + heldPhotoIndex);
    }

    /// <summary>
    /// Toggle the state of the current held photo and update the material of the photo object.
    /// </summary>
    public void SwapPhotoContent()
    {
        heldPhoto.ToggleState();
        photoObject.GetComponent<MeshRenderer>().material = heldPhoto.GetMaterial_Current();
    }
    #endregion
}
