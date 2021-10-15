using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <class>Interactable</class>
/// Script for interactable logic. For use with both pickups and other interactable objects.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class Interactable : MonoBehaviour
{
    #region fields
    public float radius = 2.0f;
    public GameObject UIElement;
    public bool requiresKey = false;
    public Vector3 rot;
    public Vector3 pos;
    #endregion

    void Start()
    {

    }

    void Update()
    {

    }

    #region helper methods
    /// <summary>
    /// While within the radius, on mouse over, bring up the GUI element. If the Interact key is pressed, do this object's interaction.
    /// </summary>
    private void OnMouseOver()
    {
        if(Vector3.Distance(FPController.instance.transform.position, this.gameObject.transform.position) <= radius)
        {
            if(requiresKey)
            {
                if(FPController.instance.hasKey)
                {
                    TempGUI.gui.TurnOnTutorial(TutorialType.PICKUP);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Interaction();
                    }
                }
                else
                {
                    TempGUI.gui.TurnOnTutorial(TutorialType.PICKUP);
                }
            }
            else
            {
                TempGUI.gui.TurnOnTutorial(TutorialType.PICKUP);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Interaction();
                }
            }
        }
    }

    /// <summary>
    /// Turns off the GUI element on mouse exit.
    /// </summary>
    void OnMouseExit()
    {
        TempGUI.gui.TurnOffTutorial(TutorialType.PICKUP);
    }

    /// <summary>
    /// Method called when the interact button is pressed while the player's mouse is hovering over this GameObject.
    /// </summary>
    void Interaction()
    {
        if (this.CompareTag("Pickup"))
        {
            TempGUI.gui.TurnOffTutorial(TutorialType.PICKUP);
            UIElement.SetActive(true);
            FPController.instance.hasKey = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            if (requiresKey)
            {
                if(!FPController.instance.hasKey)
                {
                    return;
                }

                TempGUI.gui.TurnOffTutorial(TutorialType.PICKUP);
                if (pos != Vector3.zero)
                {
                    this.transform.position = pos;
                }
                else if (rot != Vector3.zero)
                {
                    this.transform.rotation = Quaternion.Euler(rot);
                }
            }     
        }
    }
    #endregion
}
