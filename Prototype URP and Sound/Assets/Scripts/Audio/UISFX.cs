using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains methods for playing instances of UI sound effects.
/// </summary>
public class UISFX : MonoBehaviour
{
    #region Fields
    private FMOD.Studio.EventInstance startGame;
    private FMOD.Studio.EventInstance select;
    private FMOD.Studio.EventInstance hoverOn;
    private FMOD.Studio.EventInstance hoverOff;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

#region Helper Methods
    public void PlaySelect()
    {
        select = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/UI Select");
        select.start();
        select.release();
    }

    public void PlayHoverOn()
    {
        hoverOn = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/UI Hover On");
        hoverOn.start();
        hoverOn.release();
    }

    public void PlayHoverOff()
    {
        hoverOff = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/UI Hover Off");
        hoverOff.start();
        hoverOff.release();
    }

    public void PlayStartGame()
    {
        startGame = FMODUnity.RuntimeManager.CreateInstance("event:/Menus/Game Start");
        startGame.start();
        startGame.release();
    }
#endregion
}
