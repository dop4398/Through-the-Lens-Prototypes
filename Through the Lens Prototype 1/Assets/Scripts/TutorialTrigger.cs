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
        if(type == TutorialType.GATHER)
        {
            // once all of the photos are picked up, deactivate the tutorial GUI colliders.
            if (FPController.instance.album.Count == 3)
            {
                TempGUI.gui.TurnOffTutorial(type);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TempGUI.gui.TurnOnTutorial(type);

            if (duration > 0f)
            {
                Invoke("TurnOff", duration);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            CancelInvoke("TurnOff");

            switch (type)
            {
                case TutorialType.HOLDPHOTO:
                    TempGUI.gui.TurnOffTutorial(TutorialType.LINEUP);
                    break;
            }

            TempGUI.gui.TurnOffTutorial(type);

            if (triggerOnce)
            {
                Invoke("RemoveSelf", 2f);
            }
        }
    }

    private void TurnOff()
    {
        TempGUI.gui.TurnOffTutorial(type);
    }

    private void RemoveSelf()
    {
        TempGUI.gui.TurnOffTutorial(type);
        gameObject.SetActive(false);
    }
}
