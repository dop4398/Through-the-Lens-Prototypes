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
    public string ID { get; set; }
    public Material mat_old;
    public Material mat_new;

    //public enum State
    //{
    //    Past,
    //    Present
    //}
    public PhotoState state;
    #endregion

    void Start()
    {
        state = PhotoState.Past;

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
    /// Getter for the photo's material (the picture itself).
    /// </summary>
    /// <returns>Material for the photo image.</returns>
    public Material GetMaterial_Old()
    {
        return mat_old;
    }
    public Material GetMaterial_New()
    {
        return mat_new;
    }
    public Material GetMaterial_Current()
    {
        return state == PhotoState.Past ? mat_old : mat_new;
    }

    /// <summary>
    /// Toggle the current state of this photo
    /// </summary>
    public void ToggleState()
    {
        if (state == PhotoState.Past)
            state = PhotoState.Present;
        else
            state = PhotoState.Past;
    }
    #endregion
}
