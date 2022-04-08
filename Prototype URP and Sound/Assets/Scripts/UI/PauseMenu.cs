using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject option;

    private void Awake()
    {
        GameManager.OnGamePaused += OnGameManagerPause;
    }

    private void OnDestroy()
    {
        GameManager.OnGamePaused -= OnGameManagerPause;
    }

    private void OnGameManagerPause(bool pause)
    {
        main.SetActive(true);
        option.SetActive(false);
    }
}
