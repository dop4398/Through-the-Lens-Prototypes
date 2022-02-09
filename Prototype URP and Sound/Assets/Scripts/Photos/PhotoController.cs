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

    [SerializeField]
    [Range(1f, 2f)]
    private float PulseMagnitude;

    [SerializeField]
    [Range(1f, 2f)]
    private float PulseFrequency;

    public ParticleController successParticle;

    private Tweener dissolveTween;
    private Tweener PulseTween;
    private float pulse_modifier = 1.0f;

    public bool isDissolving
    {
        get
        {
            if (dissolveTween == null)
                return false;

            return dissolveTween.IsPlaying();
        }
    }

    public bool isPulsing
    {
        get
        {
            if (PulseTween == null)
                return false;

            return PulseTween.IsPlaying();
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
        Debug.Log("PhotoController State: " + state);
        SetState(CharacterComponents.instance.heldPhoto.heldPhoto.state);

        Debug.Log("PhotoController State: " + state);
        Debug.Log("HeldPhoto State: " + CharacterComponents.instance.heldPhoto.heldPhoto.state);

        //Dissolve tween effect init
        dissolveTween = DOVirtual.Float(0f, 0.85f, TransitionTime, v =>
        {
            time = v;
            material.SetFloat("_T", v);
        }).SetAutoKill(false);

        //Pulse tween effect init
        PulseTween = DOVirtual.Float(1f, PulseMagnitude, PulseFrequency, v =>
        {
            pulse_modifier = v;
        }).SetAutoKill(false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        dissolveTween.Pause();
        PulseTween.Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //ChangeState();
            PlaySuccessParticle();
        }

        if(state != CharacterComponents.instance.heldPhoto.heldPhoto.state)
        {
            ChangeState();
        }
    }

    public void Pulse(bool b)
    {
        if (b)
        {
            PulseTween.Play();
        }
        else
        {
            PulseTween.Pause();
            PulseTween.Goto(0f);
        }
    }

    public void SetPulseSpeed(float f)
    {
        f = Mathf.Clamp(f, 0f, 1f);
        PulseTween.timeScale = 1f + f;
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
        intensity = intensity * glow_intensity * pulse_modifier;
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
                Past_D();
                break;
            case PhotoState.Present:
                Present_D();
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

        time = 0f;
        state = PhotoState.Present;
    }

    //Get Tween Status
    public bool GetPhotoStatus()
    {
        return !dissolveTween.IsPlaying();
    }

    public void PlaySuccessParticle()
    {
        successParticle.Play();
    }
    #endregion

}
