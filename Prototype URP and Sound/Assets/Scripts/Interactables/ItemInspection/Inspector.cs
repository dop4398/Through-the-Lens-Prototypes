using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    public static Inspector instance;

    public Camera camera;
    [HideInInspector]
    public InspectionLoader loader;
    [HideInInspector]
    public InspectionControl controller;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        loader = GetComponent<InspectionLoader>();
        controller = GetComponent<InspectionControl>();

        EventSystem.instance.OnItemInspection += TurnCameraOn;
        EventSystem.instance.OnItemInspection += TurnIndicatorOff;
        EventSystem.instance.OnItemInspection += () =>
        {
            GameManager.instance.UpdateGameState(GameState.Inspection);
        };
        EventSystem.instance.OnItemInspectionExit += TurnCameraOff;
        EventSystem.instance.OnItemInspectionExit += TurnIndicatorOn;
        EventSystem.instance.OnItemInspectionExit += () => {
            GameManager.instance.UpdateGameState(GameState.Game);
        };
    }

    void TurnCameraOn()
    {
        camera.enabled = true;
    }

    void TurnCameraOff()
    {
        camera.enabled = false;
    }

    void TurnIndicatorOn()
    {
        Indicator.instance.gameObject.SetActive(true);
    }

    void TurnIndicatorOff()
    {
        Indicator.instance.gameObject.SetActive(false);
    }
}
