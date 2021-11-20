using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Thrown together script for players to quit the game gracefully rather than force quitting
/// </summary>
/// <author>
/// Abigail Markish
/// </author>
public class QuitMenu : MonoBehaviour
{

    public static bool menuActive = false;  //general boolean for decision making
    public GameObject pullUpMenu;   //part of canvas that needs to be activated/toggled

    // Start is called before the first frame update
    void Start()
    {
        //none
    }

    // Update is called once per frame
    void Update()
    {
        //if menu is active on escape key, inactivate it. If inactive, activate
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive)
            {
                CloseMenu();    //inactivate
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                PopMenu();  //activate
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            
        }
    }

    #region Helper Methods
    private void CloseMenu()    //deactivate the menu object and set bool to false
    {
        pullUpMenu.SetActive(false);
        menuActive = false;
        Time.timeScale = 1.0f;
    }

    private void PopMenu()  //activate the menu object and set bool to true
    {
        pullUpMenu.SetActive(true);
        menuActive = true;
        Time.timeScale = 0.0f;
    }

    public void EndGame()   //method for OnClick(), on button press, quit the game (needs EventSystem to function)
    {
        Application.OpenURL("https://rit.az1.qualtrics.com/jfe/form/SV_0PzqSprj4gyQIXs");
        Application.Quit();
    }
    #endregion
}
