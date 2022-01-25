using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMaker : MonoBehaviour
{

    public bool active;

    [Header("Screenshot Frame")]
    [SerializeField]
    [Range(100, 1000)]
    public float centerX;
    [SerializeField]
    [Range(100, 1000)]
    public float centerY;
    [SerializeField]
    [Range(100, 1000)]
    public float sizeX;
    [SerializeField]
    [Range(100, 1000)]
    public float sizeY;

    [Header("GUI Skins")]
    public GUISkin normal;
    public GUISkin photo;

    private bool flag = false;
    private bool isPhotoMode = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            flag = !flag;
        }
    }

    private void OnGUI()
    {
        if (!active)
            return;

        GUI.skin = normal;

        // Make a background box
        GUI.Box(new Rect(10, 10, 140, 70), "Cool Dev Tool");

        //Unlock Cursor while holding down TAB
        if (flag)
        {
            CharacterComponents.instance.controller.canLook = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            CharacterComponents.instance.controller.canLook = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        isPhotoMode = GUI.Toggle(new Rect(20, 30, 120, 20), isPhotoMode, "Screenshot Mode");

        // Make the second button.
        if (GUI.Button(new Rect(20, 50, 100, 20), "Make Station"))
        {
            
        }

        GUI.skin = photo;

        if (isPhotoMode)
        {
            GUI.Box(new Rect(centerX, centerY, sizeX, sizeY), GUIContent.none);
        }
    }

    private void MakeStation()
    {
        GameObject g = new GameObject("New Station");
        Station s = g.AddComponent<Station>();
        s.state = Station.State.present;


    }
}
