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
    //[SerializeField]
    //private float t = 0.5f;
    [SerializeField]
    private float yPos = 1.64f;
    private float stepOffset = 0.1f;
    //private bool togglePlaying = false;
    private Sequence walkSequence;
    private Sequence runSequence;
    #endregion

    void Start()
    {
        yPos = gameObject.transform.position.y;

        // Make the walk and run sequences
        CreateWalkSequence(yPos, stepOffset, CharacterComponents.instance.footstepsSFX.WalkSpeed);
        walkSequence.Pause();
        CreateRunSequence(yPos, stepOffset, CharacterComponents.instance.footstepsSFX.RunSpeed);
        runSequence.Pause();
    }

    void Update()
    {
        // If the player is moving:
        if (PlayerInput.playerInput.input != Vector2.zero)
        {
            HeadBobbing();
        }
    }


    #region Helper Methods
    /// <summary>
    /// Creates the walk sequence based on the transform's initial y position and the FootstepsSFX walk speed.
    /// </summary>
    /// <returns>The new walk sequence.</returns>
    private Sequence CreateWalkSequence(float y, float offset, float duration)
    {
        walkSequence = DOTween.Sequence();
        walkSequence.Append(gameObject.transform.DOLocalMoveY(y - offset, duration).SetEase(Ease.InOutQuad).SetAutoKill(false));
        walkSequence.Append(gameObject.transform.DOLocalMoveY(y, duration).SetEase(Ease.InOutQuad).SetAutoKill(false));
        walkSequence.SetAutoKill(false);
        return walkSequence;
    }

    /// <summary>
    /// Creates the run sequence based on the transform's initial y position and the FootstepsSFX run speed.
    /// </summary>
    /// <returns>The new run sequence.</returns>
    private Sequence CreateRunSequence(float y, float offset, float duration)
    {
        runSequence = DOTween.Sequence();
        runSequence.Append(gameObject.transform.DOLocalMoveY(y - offset, duration).SetEase(Ease.InOutQuad).SetAutoKill(false));
        runSequence.Append(gameObject.transform.DOLocalMoveY(y, duration).SetEase(Ease.InOutQuad).SetAutoKill(false));
        runSequence.SetAutoKill(false);
        return runSequence;
    }

    /// <summary>
    /// Plays the walk sequence when walking and the run sequence when running.
    /// Completes the active sequence when the player stops input.
    /// </summary>
    private void HeadBobbing()
    {
        Debug.Log("walk sequence: " + walkSequence.IsPlaying() + "\nrun sequence: " + runSequence.IsPlaying());
        // if no sequence is playing:
        if (!walkSequence.IsPlaying() && !runSequence.IsPlaying())
        {
            if (PlayerInput.playerInput.run)
            {
                //DOTween.Play(runSequence);
                runSequence.Restart();
            }
            else
            {
                //DOTween.Play(walkSequence);
                walkSequence.Restart();
            }
        }

        // Shifting from walking to running currently jumps instead of smoothly transitions.

        // If walk is playing and playerInput.run becomes true:
        if (walkSequence.IsPlaying() && PlayerInput.playerInput.run)
        {
            walkSequence.Complete();
            //DOTween.Play(runSequence);
            runSequence.Restart();
        }

        // If run is playing and playerInput.run becomes false:
        if (runSequence.IsPlaying() && !PlayerInput.playerInput.run)
        {
            runSequence.Complete();
            //DOTween.Play(walkSequence);
            walkSequence.Restart();
        }
    }
    #endregion
}
