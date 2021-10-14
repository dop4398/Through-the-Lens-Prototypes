using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <class>Interactable</class>
/// Script for interactable logic. For use with both pickups and other interactable objects.
/// </summary>
/// <authour>
/// David Patch
/// </authour>
public class Interactable : MonoBehaviour
{
    #region fields
    public float radius = 2.0f;
    //public Color m_MouseOverColor = Color.red;
    //private Color m_OriginalColor;
    //private MeshRenderer m_Renderer;
    #endregion

    void Start()
    {
        //m_Renderer = GetComponent<MeshRenderer>();
        //m_OriginalColor = m_Renderer.material.color;
    }

    void Update()
    {

    }

    #region helper methods
    /// <summary>
    /// While within the radius, on mouse over, change the color of the GameObject. If the Interact key is pressed, do this object's interaction.
    /// </summary>
    private void OnMouseOver()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= radius)
        {
            //m_Renderer.material.color = m_MouseOverColor;
            TempGUI.gui.TurnOnTutorial(TutorialType.PICKUP);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Interaction();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseExit()
    {
        //m_Renderer.material.color = m_OriginalColor;
        TempGUI.gui.TurnOffTutorial(TutorialType.PICKUP);
    }

    /// <summary>
    /// Method called when the interact button is pressed while the player's mouse is hovering over this GameObject.
    /// </summary>
    void Interaction()
    {
        
    }
    #endregion
}
