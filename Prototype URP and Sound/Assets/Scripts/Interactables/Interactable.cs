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
    #region Fields
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

    #region Helper Methods
    /// <summary>
    /// While within the radius, on mouse over, bring up the GUI element. If the Interact key is pressed, do this object's interaction.
    /// </summary>
    private void OnMouseOver()
    {
        if(Vector3.Distance(CharacterComponents.instance.controller.transform.position, this.gameObject.transform.position) <= radius)
        {
            if(requiresKey)
            {
                //if(CharacterComponents.instance.controller.hasKey)
                //{
                //    TempGUI.gui.TurnOnTutorial(TutorialType.PICKUP);
                //    if (Input.GetKeyDown(KeyCode.Mouse0))
                //    {
                //        Interaction();
                //    }
                //}
                //else
                //{
                //    TempGUI.gui.TurnOnTutorial(TutorialType.LOCKED);
                //}
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
        TempGUI.gui.TurnOffTutorial(TutorialType.LOCKED);
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
            //CharacterComponents.instance.controller.hasKey = true;
            this.gameObject.SetActive(false);
        }
        else if(this.CompareTag("Photo"))
        {
            TempGUI.gui.TurnOffTutorial(TutorialType.PICKUP);
            CharacterComponents.instance.album.AddPhoto(this.gameObject.GetComponent<Photo>());
            CharacterComponents.instance.controller.HandWithPhoto.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            //if (requiresKey)
            //{
            //    if(!CharacterComponents.instance.controller.hasKey)
            //    {
            //        return;
            //    }

            //    TempGUI.gui.TurnOffTutorial(TutorialType.PICKUP);
            //    if (pos != Vector3.zero)
            //    {
            //        this.transform.position = pos;
            //    }
            //    else if (rot != Vector3.zero)
            //    {
            //        this.transform.rotation = Quaternion.Euler(rot);
            //    }
            //}     
        }
    }
    #endregion
}
