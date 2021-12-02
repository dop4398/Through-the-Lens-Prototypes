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
    public Photo heldPhoto;
    public int heldPhotoIndex;
    [SerializeField] private bool photoInFocus;

    // placeholder vectors to be phased out when we put the held photo on the UI
    public Vector3 restPosition;
    public Vector3 focusedPosition;
    #endregion

    void Start()
    {
        photoInFocus = false;
        restPosition = new Vector3(0.75f, -0.4f, 1.5f);
        focusedPosition = new Vector3(0, 0, 0.5f);
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
    /// This method directly interacts with the PhotoController instance.
    /// </summary>
    /// <param name="photo"></param>
    public void SetHeldPhoto(Photo photo)
    {
        heldPhoto = photo;

        // Update the photo object being held
        PhotoController.instance.SetMainTexture(heldPhoto.GetMaterial_Old().mainTexture);
        PhotoController.instance.SetSubTexture(heldPhoto.GetMaterial_New().mainTexture);
        PhotoController.instance.SetState(heldPhoto.state);
    }

    /// <summary>
    /// Bring the currently held photo into focus in the center of the screen, blocking a good section of the player's vision.
    /// </summary>
    public void FocusPhoto()
    {
        PhotoController.instance.transform.localPosition = focusedPosition; // physically moving the instance here.
        photoInFocus = true;
    }

    /// <summary>
    /// Remove the photo from the center of the screen. Brings it out of focus.
    /// </summary>
    public void UnfocusPhoto()
    {
        PhotoController.instance.transform.localPosition = restPosition;
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

        if(CharacterComponents.instance.album.GetAlbumSize() > 0)
        {
            SetHeldPhoto(CharacterComponents.instance.album.GetPhotoAtIndex(heldPhotoIndex));

            Debug.Log(heldPhoto.ID + " - " + heldPhotoIndex);
        }
    }

    /// <summary>
    /// Toggle the state of the current held photo and update the material of the photo object.
    /// </summary>
    public void SwapPhotoContent()
    {
        heldPhoto.ToggleState();

        //PhotoController.instance.GetComponent<MeshRenderer>().material = heldPhoto.GetMaterial_Current();
        PhotoController.instance.ChangeState();
    }
    #endregion
}
