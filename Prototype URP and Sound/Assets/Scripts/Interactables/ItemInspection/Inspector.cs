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
        EventSystem.instance.OnItemInspectionExit += TurnCameraOff;
    }

    void TurnCameraOn()
    {
        camera.enabled = true;
    }

    void TurnCameraOff()
    {
        camera.enabled = false;
    }
}
