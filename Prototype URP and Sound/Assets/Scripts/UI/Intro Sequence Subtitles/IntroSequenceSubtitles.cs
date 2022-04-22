using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequenceSubtitles : MonoBehaviour
{
    public SubScripts subtitles;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlaySubs0()
    {
        subtitles.tutorialRun(0);
    }

    public void PlaySubs1()
    {
        subtitles.tutorialRun(1);
    }

    public void PlaySubs2()
    {
        subtitles.tutorialRun(2);
    }
}
