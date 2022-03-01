using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action OnItemInspection;
    public void ItemInspection()
    {
        if (OnItemInspection != null)
            OnItemInspection();
    }

    public event Action OnItemInspectionExit;
    public void ItemInspectionExit()
    {
        if (OnItemInspectionExit != null)
            OnItemInspectionExit();
    }

}
