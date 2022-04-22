using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public int index;
    public SubScripts subtitles;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "FPPlayer Variant")
        {
            subtitles.tutorialRun(index);
            gameObject.SetActive(false);
        }
       
    }
}
