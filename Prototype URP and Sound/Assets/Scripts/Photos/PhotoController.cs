using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PhotoState
{
    Past,
    Present
}

/// <summary>
/// Singleton instance that controls the photo scene in the player's hand. 
/// </summary>
/// <author>
/// Alfie Luo
/// </author>
public class PhotoController : MonoBehaviour
{
    public static PhotoController instance;

    #region Fields
    private Material material;
    public PhotoState state;

    [SerializeField]
    [Range(1f, 10f)]
    private float glow_intensity;
    private float time;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float TransitionTime;

    private Tweener dissolveTween;

    public bool isTweening
    {
        get
        {
            if (dissolveTween == null)
                return false;

            return dissolveTween.IsPlaying();
        }
    }
    #endregion

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //DOTween init
        DOTween.Init(true, true);

        //Other init
        material = gameObject.GetComponent<MeshRenderer>().material;
        state = PhotoState.Past;

        //Tween effect init
        dissolveTween = DOVirtual.Float(0f, 0.85f, TransitionTime, v =>
        {
            time = v;
            material.SetFloat("_T", v);
        }).SetAutoKill(false);

        dissolveTween.Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeState();
        }
    }

    #region Helper Functions
    public void SetMainTexture(Texture t)
    {
        material.SetTexture("_MainTex", t);
    }

    public void SetSubTexture(Texture t)
    {
        material.SetTexture("_SubTex", t);
    }

    //Set frame's glow intensity
    public void SetGlow(float intensity)
    {
        intensity = intensity * glow_intensity;
        material.SetFloat("_EmissionIntensity", intensity);
    }

    //Change state based on the current state
    public void ChangeState()
    {
        switch (state)
        {
            case PhotoState.Past:
                Present_T();
                break;
            case PhotoState.Present:
                Past_T();
                break;
            default:
                break;
        }
    }

    //Tween to past state
    public void Past_T()
    {
        if (dissolveTween.IsPlaying())
            return;

        dissolveTween.PlayBackwards();

        state = PhotoState.Past;
    }

    //Tween to present state
    public void Present_T()
    {
        if (dissolveTween.IsPlaying())
            return;

        dissolveTween.PlayForward();

        state = PhotoState.Present;
    }

    public void SetState(PhotoState state)
    {
        switch (state)
        {
            case PhotoState.Past:
                Present_D();
                break;
            case PhotoState.Present:
                Past_D();
                break;
            default:
                break;
        }
    }

    //Switch to present state
    public void Past_D()
    {
        //Pause the current tween and go to past state
        dissolveTween.Pause();
        dissolveTween.Goto(0f);

        time = 0f;
        state = PhotoState.Past;
    }

    //Switch to present state
    public void Present_D()
    {
        //Pause the current tween and go to present state
        dissolveTween.Pause();
        dissolveTween.Goto(TransitionTime);

        time = 1f;
        state = PhotoState.Present;
    }

    //Get Tween Status
    public bool GetPhotoStatus()
    {
        return !dissolveTween.IsPlaying();
    }
    #endregion

}
