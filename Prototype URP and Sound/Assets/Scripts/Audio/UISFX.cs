using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains methods for playing instances of UI sound effects.
/// </summary>
public class UISFX : MonoBehaviour
{
    #region Fields
    private FMOD.Studio.EventInstance select;
    private FMOD.Studio.EventInstance hoverOn;
    private FMOD.Studio.EventInstance hoverOff;
    #endregion

    void Start()
    {
        select = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/UI Select");
        hoverOn = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/UI Hover On");
        hoverOff = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/UI Hover Off");
    }

    void Update()
    {
        
    }

#region Helper Methods
    public void PlaySelect()
    {
        select.start();
        select.release();
    }

    public void PlayHoverOn()
    {
        hoverOn.start();
        hoverOn.release();
    }

    public void PlayHoverOff()
    {
        hoverOff.start();
        hoverOff.release();
    }
#endregion
}
