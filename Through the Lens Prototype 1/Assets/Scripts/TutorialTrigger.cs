using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialType type;
    public bool triggerOnce;
    public float duration;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TempGUI.gui.TurnOnTutorial(type);

            if (duration > 0f)
            {
                CancelInvoke("TurnOff");
                Invoke("TurnOff", duration);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CancelInvoke("TurnOff");

            TempGUI.gui.TurnOffTutorial(type);
            if (triggerOnce)
                gameObject.SetActive(false);
        }
    }

    private void TurnOff()
    {
        TempGUI.gui.TurnOffTutorial(type);
    }
}
