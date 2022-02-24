using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public bool photoInFocus;
    [HideInInspector]
    public bool swapHasTriggered;
    private bool photoLoaded;

    // placeholder vectors to be phased out when we put the held photo on the UI
    public Vector3 restPosition;
    public Vector3 focusedPosition;
    public float focusTime;

    private Tweener swapTween;
    #endregion

    void Start()
    {
        photoInFocus = false;
        restPosition = new Vector3(0.34f, -0.25f, 0.64f);
        focusedPosition = new Vector3(0, 0, 0.185f);
        swapHasTriggered = false;

        swapTween = DOVirtual.Float(restPosition.y - 0.1f, restPosition.y, 0.5f, y => { PhotoController.instance.transform.localPosition = new Vector3(restPosition.x, y, restPosition.z); }).Pause().SetEase(Ease.InOutQuad).SetAutoKill(false);

        // If the player starts with any photos, the first one they have in the album will be set as the held photo.
        LoadFirstPhoto();
    }

    void Update()
    {
        LoadFirstPhoto();

        if(CharacterComponents.instance.playerstate.GetState() == PlayerState.normal)
        {
            if (PlayerInput.playerInput.swap != 0)
            {
                CyclePhoto(PlayerInput.playerInput.swap);
            }
            if (PlayerInput.playerInput.focusPhoto)
            {
                FocusPhoto();
            }
            if (PlayerInput.playerInput.unfocusPhoto)
            {
                UnfocusPhoto();
            }
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
        PhotoController.instance.SetMainTexture(heldPhoto.GetTexture_Old());
        PhotoController.instance.SetSubTexture(heldPhoto.GetTexture_New());
        PhotoController.instance.SetState(heldPhoto.state);
    }

    /// <summary>
    /// Bring the currently held photo into focus in the center of the screen, blocking a good section of the player's vision.
    /// </summary>
    public void FocusPhoto()
    {
        //PhotoController.instance.transform.localPosition = focusedPosition; // physically moving the instance here.
        PhotoController.instance.transform.DOKill();
        PhotoController.instance.transform.DOLocalMove(focusedPosition, focusTime).SetEase(Ease.OutCubic).OnComplete(() => SetFocusState(true)).SetAutoKill(false);
        //photoInFocus = true;
    }

    public void SetFocusState(bool state)
    {
        photoInFocus = state;
        if (state == false)
            swapHasTriggered = false;
    }

    /// <summary>
    /// Remove the photo from the center of the screen. Brings it out of focus.
    /// </summary>
    public void UnfocusPhoto()
    {
        //PhotoController.instance.transform.localPosition = restPosition;
        //photoInFocus = false;
        //swapHasTriggered = false;
        PhotoController.instance.transform.DOKill();
        PhotoController.instance.transform.DOLocalMove(restPosition, focusTime).SetEase(Ease.OutCubic).OnStart(() => SetFocusState(false)).SetAutoKill(false);
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
    public void CyclePhoto(int direction)
    {
        if (IsInFocus())
            return;

        swapTween.Restart();

        if (direction >= 0)
        {
            heldPhotoIndex++;
        }
        else
        {
            heldPhotoIndex--;
        }

        if (heldPhotoIndex >= CharacterComponents.instance.album.GetAlbumSize())
        {
            heldPhotoIndex = 0;
        }

        if (heldPhotoIndex < 0)
        {
            heldPhotoIndex = CharacterComponents.instance.album.GetAlbumSize() - 1;
        }

        if (CharacterComponents.instance.album.GetAlbumSize() > 0)
        {
            SetHeldPhoto(CharacterComponents.instance.album.GetPhotoAtIndex(heldPhotoIndex));
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


    public void LoadFirstPhoto()
    {
        if (!photoLoaded && CharacterComponents.instance.album.GetAlbumSize() > 0)
        {
            heldPhotoIndex = 0;
            SetHeldPhoto(CharacterComponents.instance.album.GetPhotoAtIndex(heldPhotoIndex));
            photoLoaded = true;
        }
    }
    #endregion
}
