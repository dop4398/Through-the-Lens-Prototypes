using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    normal,
    restricted,
    inventory,
    inspecting,
    dev_design
}
/// <summary>
/// update summary and function headers
/// </summary>
public class CharacterState : MonoBehaviour
{
    [SerializeField]
    private PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.normal;
    }

    public void SetState(PlayerState s)
    {
        Debug.Log(s);
        state = s;
        switch (s)
        {
            case PlayerState.normal:
            case PlayerState.restricted:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case PlayerState.inventory:
            case PlayerState.inspecting:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public PlayerState GetState()
    {
        return state;
    }

}
