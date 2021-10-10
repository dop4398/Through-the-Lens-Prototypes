using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialType type;
    public bool triggerOnce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TempGUI.gui.TurnOnTutorial(type);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TempGUI.gui.TurnOffTutorial(type);
            if (triggerOnce)
                gameObject.SetActive(false);
        }
    }
}
