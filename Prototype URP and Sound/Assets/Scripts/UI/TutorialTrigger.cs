using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public int index;
    //private bool playerCollision;
    public SubScripts subtitles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "FPPlayer Variant")
        {
            Debug.Log("Hello");
            subtitles.tutorialRun(index);
        }
       
    }
}
