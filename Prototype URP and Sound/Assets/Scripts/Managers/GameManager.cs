using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    Mainmenu,
    Pausemenu,
    Game,
    Inventory,
    Album,
    Inspection
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        UpdateGameState(GameState.Game);
    }

    public void UpdateGameState(string newState)
    {
        UpdateGameState((GameState)System.Enum.Parse(typeof(GameState), newState));
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            case GameState.Game:
                CharacterComponents.instance.playerstate.SetState(PlayerState.normal);
                break;
            case GameState.Mainmenu:
                CharacterComponents.instance.playerstate.SetState(PlayerState.ui);
                break;
            case GameState.Pausemenu:
                CharacterComponents.instance.playerstate.SetState(PlayerState.ui);
                break;
            case GameState.Inventory:
                CharacterComponents.instance.playerstate.SetState(PlayerState.ui);
                break;
            case GameState.Album:
                CharacterComponents.instance.playerstate.SetState(PlayerState.ui);
                break;
            case GameState.Inspection:
                CharacterComponents.instance.playerstate.SetState(PlayerState.inspecting);
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
