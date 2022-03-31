using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple object to store an index that is paired with a Locus.
/// </summary>
/// <author>
/// David Patch
/// </author>

[System.Serializable]
public class Photo
{
    #region Fields
    public string ID;
    public Texture2D tex_old;
    public Texture2D tex_new;
    public PhotoState state;
    #endregion

    #region Helper Methods
    /// <summary>
    /// Getter for the photo's material (the picture itself).
    /// </summary>
    /// <returns>Material for the photo image.</returns>
    /// 
    public Photo(Photo photo)
    {
        ID = photo.ID;
        tex_old = photo.tex_old;
        tex_new = photo.tex_new;
        state = photo.state;
    }
    public Texture2D GetTexture_Old()
    {
        return tex_old;
    }
    public Texture2D GetTexture_New()
    {
        return tex_new;
    }
    public Texture2D GetTexture_Current()
    {
        return state == PhotoState.Past ? tex_old : tex_new;
    }

    /// <summary>
    /// Toggle the current state of this photo.
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
