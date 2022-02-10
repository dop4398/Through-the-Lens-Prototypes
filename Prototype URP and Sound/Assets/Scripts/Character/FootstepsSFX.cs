using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays footstep sound effects based on the type of surface that the player is standing on.
/// Sourced from <a href="https://alessandrofama.com/tutorials/fmod/unity/footsteps">this article</a> by Alessandro Famà.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class FootstepsSFX : MonoBehaviour
{
    #region Fields
    private enum CURRENT_TERRAIN { GRASS, PAVEMENT };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;
    private FMOD.Studio.EventInstance footsteps;
    private float timer = 0.0f;
    [SerializeField]
    private float footstepSpeed = 0.75f;
    [SerializeField]
    private float walkSpeed = 0.75f;
    [SerializeField]
    private float runSpeed = 0.25f;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        DetermineTerrain();

        if (PlayerInput.playerInput.input != Vector2.zero 
            && CharacterComponents.instance.controller.GetComponent<CharacterController>().isGrounded)
        {
            // If left shift is held, use the run speed instead
            footstepSpeed = PlayerInput.playerInput.run ? runSpeed : walkSpeed;

            if(timer > footstepSpeed)
            {
                SelectAndPlayFootstep();
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }
    }

    #region Helper Methods
    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach(RaycastHit rayhit in hit)
        {
            if(rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = CURRENT_TERRAIN.GRASS;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Pavement"))
            {
                currentTerrain = CURRENT_TERRAIN.PAVEMENT;
            }
        }

    }

    public void SelectAndPlayFootstep()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRASS:
                PlayFootstep(0);
                break;
            case CURRENT_TERRAIN.PAVEMENT:
                PlayFootstep(1);
                break;
            default:
                PlayFootstep(0);
                break;
        }
    }

    private void PlayFootstep(int terrain)
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps");
        footsteps.setParameterByName("Terrain", terrain);
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footsteps.start();
        footsteps.release();
    }

    public float GetFootstepSpeed()
    {
        return footstepSpeed;
    }
    #endregion
}
