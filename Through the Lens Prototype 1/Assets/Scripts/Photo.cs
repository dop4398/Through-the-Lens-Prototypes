using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple object to store an index that is paired with a Locus.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class Photo : MonoBehaviour
{
    #region Fields
    [SerializeField] private string locusID = "";
    private GameObject heldPhoto;
    [SerializeField] private Material mat;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Helper Methods
    /// <summary>
    /// Getter for the photo's locus ID.
    /// </summary>
    /// <returns>ID String.</returns>
    public string GetID()
    {
        return locusID;
    }

    /// <summary>
    /// Getter for the photo's material (the picture itself).
    /// </summary>
    /// <returns>Material for the photo image.</returns>
    public Material GetMaterial()
    {
        return mat;
    }
    #endregion
}
