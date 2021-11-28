using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhotoController : MonoBehaviour
{

    public static PhotoController instance;

    #region Fields
    private Material material;
    public State state;
    private float time;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float TransitionTime;

    private Tweener dissolveTween;

    public enum State
    {
        Past,
        Present
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
        state = State.Past;

        //Tween effect init
        dissolveTween = DOVirtual.Float(0f, 0.85f, 2f, v =>
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
        intensity = intensity * 100f;
        material.SetFloat("_EmissionIntensity", intensity);
    }

    //Change state based on the current state
    public void ChangeState()
    {
        switch (state)
        {
            case State.Past:
                Present_T();
                break;
            case State.Present:
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

        state = State.Past;
    }

    //Tween to present state
    public void Present_T()
    {
        if (dissolveTween.IsPlaying())
            return;

        dissolveTween.PlayForward();

        state = State.Present;
    }

    //Switch to present state
    public void Past_D()
    {
        time = 0f;
    }

    //Switch to present state
    public void Present_D()
    {
        time = 1f;
    }

    //Get Tween Status
    public bool GetPhotoStatus()
    {
        return !dissolveTween.IsPlaying();
    }
    #endregion

}
