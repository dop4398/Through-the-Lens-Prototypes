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
    [SerializeField] private string ID = "";
    public PhotoController photoController;

    public enum State
    {
        Past,
        Present
    }

    [SerializeField] private Material mat_old;
    [SerializeField] private Material mat_new;

    public State state = State.Past;
    #endregion

    private void Awake()
    {
        //photoController = GetComponent<PhotoController>();
    }

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
        return ID;
    }

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
        return state == State.Past ? mat_old : mat_new;
    }

    /// <summary>
    /// Toggle the current state of this photo
    /// </summary>
    public void ToggleState()
    {
        if (state == State.Past)
            state = State.Present;
        else
            state = State.Past;
    }
    #endregion
}
