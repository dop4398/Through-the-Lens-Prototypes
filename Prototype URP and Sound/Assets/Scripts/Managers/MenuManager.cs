using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject _PausePanel, _InventoryPanel, _AlbumPanel, _ThoughtsPanel, _IndicatorPanel;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        GameManager.OnGamePaused += GameManagerOnGamePaused;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        GameManager.OnGamePaused -= GameManagerOnGamePaused;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _InventoryPanel.SetActive(state == GameState.Inventory);
        _AlbumPanel.SetActive(state == GameState.Album);
        _ThoughtsPanel.SetActive(state == GameState.Inspection);
        _IndicatorPanel.SetActive(state == GameState.Game);
    }

    private void GameManagerOnGamePaused(bool pause)
    {
        _PausePanel.SetActive(pause);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
