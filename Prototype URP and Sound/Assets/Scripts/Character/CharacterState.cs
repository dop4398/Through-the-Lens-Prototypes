using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    normal,
    restricted,
    ui,
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

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.normal;
    }

    public void SetState(PlayerState s)
    {
        state = s;
        switch (s)
        {
            case PlayerState.normal:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerInput.playerInput.isDisabled = false;
                break;
            case PlayerState.restricted:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerInput.playerInput.isDisabled = true;
                break;
            case PlayerState.ui:
            case PlayerState.inspecting:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PlayerInput.playerInput.isDisabled = false;
                break;
            case PlayerState.dev_design:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PlayerInput.playerInput.isDisabled = true;
                break;
        }
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Album:
            case GameState.Inventory:
                SetState(PlayerState.ui);
                break;
            case GameState.Game:
                SetState(PlayerState.normal);
                break;
            case GameState.Inspection:
                SetState(PlayerState.inspecting);
                break;
        }
    }

    public PlayerState GetState()
    {
        return state;
    }

}
