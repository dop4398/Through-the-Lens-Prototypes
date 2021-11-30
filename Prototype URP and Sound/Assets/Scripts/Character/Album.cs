using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Album class holds references to all the photos that the player currently has.
/// When a photo is picked up, it is added to the Album.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class Album : MonoBehaviour
{
    #region Fields
    [SerializeField] private List<Photo> album;
    [SerializeField] private Photo currentPhoto;
    private int albumIndex = 0;
    #endregion

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    #region Helper Methods
    /// <summary>
    /// Changes the currently held photo by cycling through the list of all the player's photos (their album).
    /// This is a quick and dirty solution and should be improved upon for future prototypes.
    /// </summary>
    public void CyclePhoto()
    {
        albumIndex++;
        if (albumIndex >= album.Count)
        {
            albumIndex = 0;
        }

        currentPhoto = album[albumIndex];
        Debug.Log(currentPhoto.GetID() + " - " + albumIndex);

        //photoObject.GetComponent<MeshRenderer>().material = currentPhoto.GetMaterial_Current();
    }

    public void AddPhoto(Photo photo)
    {
        album.Add(photo);
    }

    public Photo GetCurrentPhoto()
    {
        return currentPhoto;
    }
    #endregion
}
