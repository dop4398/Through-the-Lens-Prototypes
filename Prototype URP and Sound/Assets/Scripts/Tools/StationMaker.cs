using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utility;

public class StationMaker : MonoBehaviour
{

    public bool active;

    [Header("Tool Position")]
    [SerializeField]
    [Range(10, 1000)]
    public float xPos;
    [SerializeField]
    [Range(10, 1000)]
    public float yPos;

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
        GUI.Box(new Rect(xPos + 10, yPos + 10, 140, 70), "Cool Dev Tool");

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
        isPhotoMode = GUI.Toggle(new Rect(xPos + 20, yPos + 30, 120, 20), isPhotoMode, "Screenshot Mode");

        // Make the second button.
        if (GUI.Button(new Rect(xPos + 20, yPos + 50, 100, 20), "Make Station"))
        {
            MakeStation();
        }

        GUI.skin = photo;

        if (isPhotoMode)
        {
            //Use golden radio to calculate size
            float size = Screen.width * (500f / 1256f);
            GUI.Box(new Rect(Screen.width / 2 - size / 2, Screen.height / 2 - size / 2, size, size), GUIContent.none);
            Debug.Log(Screen.width);
        }
    }

    private void MakeStation()
    {
        //New Station Object
        GameObject g = new GameObject("New_Station");

        //Add station component and set its state to present
        Station station = g.AddComponent<Station>();
        station.state = Station.State.present;

        //Added a new Trigger box object
        GameObject trigger = new GameObject("Trigger Box");

        //Add a collider to the trigger object and the trigger script
        BoxCollider box = trigger.AddComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = new Vector3(4, 1, 4);
        trigger.AddComponent<StationTrigger>();

        //Set its parent to the new station also update station's trigger variable
        trigger.transform.parent = g.transform;
        trigger.transform.position = new Vector3(0f, -0.5f, 0f);
        station.trigger = trigger;

        //Set correct radius
        station.radius = 0.25f;

        //Read the current angle and position of player and record it into the new station
        station.rot.y = CharacterComponents.instance.gameObject.transform.rotation.eulerAngles.y;
        station.rot.x = CharacterComponents.instance.controller.playerCamera.transform.rotation.eulerAngles.x;
        g.transform.position = CharacterComponents.instance.controller.playerCamera.transform.position;

        station.vignetteScalar = 1f;
        station.isSingleUse = false;

        PrefabUtil.SaveAsPrefab(g, "Assets/Resources/Prefabs/Stations/");
    }



}
