using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState{
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
    public static event Action<bool> OnGamePaused;

    private bool GameIsPaused = false;

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

    private void Update()
    {
        if (PlayerInput.playerInput.esc)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
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
                break;
            case GameState.Inventory:
                break;
            case GameState.Album:
                AlbumUI.instance.UpdateUI();
                break;
            case GameState.Inspection:
                PPVController.instance.SetDoF(true);
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        CharacterComponents.instance.playerstate.SetState(PlayerState.ui);
        OnGamePaused?.Invoke(GameIsPaused);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        UpdateGameState(State);
        OnGamePaused?.Invoke(GameIsPaused);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
