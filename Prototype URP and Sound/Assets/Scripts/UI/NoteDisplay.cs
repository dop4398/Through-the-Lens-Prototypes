using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDisplay : MonoBehaviour
{
    public static NoteDisplay instance;

    public bool active = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNote(bool b)
    {
        active = b;

        if (active)
        {
            note_view.SetActive(active);
        }
        else
        {
            note_view.SetActive(active);
        }
    }
}
