using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Bobs the players head at the same rate as footsteps.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class HeadBob : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float t = 0.5f;
    [SerializeField]
    private float yPos = 0.0f;
    private float stepOffset = 1.0f;
    private bool togglePlaying = false;
    private Sequence walkSequence;
    private Sequence runSequence;
    #endregion

    void Start()
    {
        yPos = gameObject.transform.position.y;

        // Make the necessary sequences here

    }

    void Update()
    {
        t = CharacterComponents.instance.footstepsSFX.GetFootstepSpeed();

        if (PlayerInput.playerInput.input != Vector2.zero && !togglePlaying)
        {
            //DOTween.Play(gameObject.transform);

            // The current y position is not always at yPos (full height), so sometimes a new tween starts with this lower position and makes the player stay lower
            DOTween.Kill(gameObject.transform);
            gameObject.transform.DOLocalMoveY(yPos - stepOffset, t).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
            togglePlaying = true;
            // We should try a sequence here for going down then up, so the up destination is always yPos
        }
        else if(PlayerInput.playerInput.input == Vector2.zero && togglePlaying)
        {
            //DOTween.Pause(gameObject.transform);
            // DOTween.Complete(gameObject.transform); // no effect on tweens with infinite loops

            DOTween.Kill(gameObject.transform);
            gameObject.transform.DOLocalMoveY(yPos, t).SetEase(Ease.InOutQuad); // Single tweener that we kill later, just to get us back to yPos
            togglePlaying = false;
        }
        //else if(PlayerInput.playerInput.run && togglePlaying)
        //{
        //    DOTween.Kill(gameObject.transform);
        //    gameObject.transform.DOLocalMoveY(yPos - stepOffset, t).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        //}

        // We still need cases for when a player starts running mid-walk or walking mid-run?
    }

    #region Helper Methods
    private Sequence CreateWalkSequence()
    {
        walkSequence = DOTween.Sequence();
        walkSequence.Append(gameObject.transform.DOLocalMoveY(yPos - stepOffset, t));
        return walkSequence;
    }

    private Sequence CreateRunSequence()
    {
        return runSequence;
    }
    #endregion
}
