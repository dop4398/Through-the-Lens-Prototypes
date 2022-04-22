using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    public Camera camera;
    public PlayableDirector director;
    public Playable playable;

    private bool _sceneSkipped = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_sceneSkipped)
        {
            director.time = 41f;
        }
    }

    public void GetDirector(PlayableDirector director)
    {
        _sceneSkipped = false;
        this.director = director;
    }
}
