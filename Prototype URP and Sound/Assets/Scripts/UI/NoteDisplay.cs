using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDisplay : MonoBehaviour
{
    public static NoteDisplay instance;

    private GameObject note_view;
    private NoteLoader loader;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        note_view = gameObject.transform.GetChild(0).gameObject;
        loader = note_view.GetComponentInChildren<NoteLoader>();
        EventSystem.instance.OnItemInspectionExit += Disable;
        note_view.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNote(TextAsset text)
    {
        note_view.SetActive(true);
        loader.ReadString(text);
    }

    public void Disable()
    {
        note_view.SetActive(false);
    }

    public bool IsReading()
    {
        return note_view.active;
    }
}
