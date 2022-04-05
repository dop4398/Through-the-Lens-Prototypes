using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Album class holds references to all the photos that the player currently has.
/// When a photo is picked up, it is added to the Album.
/// </summary>
/// <author>
/// David Patch & Alfie Luo
/// </author>
public class Album : MonoBehaviour
{
    #region Fields
    [SerializeField] public List<Photo> album;
    [SerializeField] public List<int> hand;
    public int max_hand = 3;

    public GameObject albumUI;
    #endregion

    void Start()
    {
        hand.Add(0);
    }

    void Update()
    {

    }

    #region Helper Methods
    public void AddPhoto(Photo photo)
    {
        album.Add(photo);
    }

    public Photo GetPhotoAtIndex(int i)
    {
        return album[i];
    }

    public int GetAlbumSize()
    {
        return album.Count;
    }

    public void AddPhotoToHand(int index)
    {
        if (!hand.Contains(index) && hand.Count < max_hand)
            hand.Add(index);
    }

    public void RemovePhotoFromHand(int index)
    {
        if (hand.Contains(index) && hand.Count > 1)
        {
            if (index == CharacterComponents.instance.album.hand[CharacterComponents.instance.heldPhoto.heldPhotoIndex])
            {
                hand.Remove(index);
                CharacterComponents.instance.heldPhoto.heldPhotoIndex = 0;
                CharacterComponents.instance.heldPhoto.SetHeldPhoto(CharacterComponents.instance.album.GetHandAtIndex(0));
            }
            else
            {
                hand.Remove(index);
            }
        }
    }

    public bool IsInHand(int index)
    {
        return hand.Contains(index);
    }

    public Photo GetHandAtIndex(int i)
    {
        return album[hand[i]];
    }

    public int GetHandSize()
    {
        return hand.Count;
    }

    public void ToggleUI()
    {
        albumUI.SetActive(!albumUI.activeSelf);
        albumUI.GetComponentInChildren<AlbumUI>().UpdateUI();
    }
    #endregion
}
