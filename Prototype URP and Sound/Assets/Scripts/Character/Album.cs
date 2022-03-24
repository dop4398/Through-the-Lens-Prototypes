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
    [SerializeField] public List<Photo> album;
    #endregion

    void Start()
    {
        
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
    #endregion
}
