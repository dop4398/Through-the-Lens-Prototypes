using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For use with photos that can be picked up in the scene.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class PhotoPickup : MonoBehaviour, IInteractable
{
    #region Fields
    //public float radius { get; set; }
    public string ID; // unique identifier for each item
    public Photo photo;
    #endregion

    private void Awake()
    {
    }

    void Start()
    {
        //radius = 1.0f;

        if (ID == null)
        {
            ID = this.name;
        }
    }

    void Update()
    {

    }

    #region Helper Methods
    /// <summary>
    /// Put it in the bag.
    /// </summary>
    public void Interaction()
    {
        // Add the item to the album
        photo.ID = ID;
        CharacterComponents.instance.album.AddPhoto(photo);

        // Set inactive in the scene
        this.gameObject.SetActive(false);
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endregion
}
