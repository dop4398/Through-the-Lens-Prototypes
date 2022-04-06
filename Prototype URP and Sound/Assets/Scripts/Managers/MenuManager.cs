using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject _PausePanel, _InventoryPanel, _AlbumPanel, _ThoughtsPanel;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _PausePanel.SetActive(state == GameState.Pausemenu);
        _InventoryPanel.SetActive(state == GameState.Inventory);
        _AlbumPanel.SetActive(state == GameState.Album);
        _ThoughtsPanel.SetActive(state == GameState.Inspection);
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
