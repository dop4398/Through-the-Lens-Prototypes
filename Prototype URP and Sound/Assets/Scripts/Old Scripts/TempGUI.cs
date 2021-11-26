using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialType
{
    WASDMOUSE,
    HOLDPHOTO,
    SWAPPHOTO,
    PICKUP,
    LOCKED
}
public class TempGUI : MonoBehaviour
{

    public static TempGUI gui;
    private string name;
    private GUIStyle style;

    public GameObject wasd;
    public GameObject hold;
    public GameObject swap;
    public GameObject pick;


    private void Awake()
    {
        gui = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        name = " ";
        style = new GUIStyle();
        style.fontSize = 25;
        style.alignment = TextAnchor.MiddleLeft;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {

        GUI.Box(new Rect(20, 20, 120, 30), "Photo: " + name, style);
    }

    public void SetName(string s)
    {
        name = s;
    }

    public void TurnOnTutorial(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.HOLDPHOTO:
                hold.SetActive(true);
                break;
            case TutorialType.PICKUP:
                pick.SetActive(true);
                break;
            case TutorialType.SWAPPHOTO:
                swap.SetActive(true);
                break;
            case TutorialType.WASDMOUSE:
                wasd.SetActive(true);
                break;
        }
    }

    public void TurnOffTutorial(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.HOLDPHOTO:
                hold.SetActive(false);
                break;
            case TutorialType.PICKUP:
                pick.SetActive(false);
                break;
            case TutorialType.SWAPPHOTO:
                swap.SetActive(false);
                break;
            case TutorialType.WASDMOUSE:
                wasd.SetActive(false);
                break;
        }
    }
}
