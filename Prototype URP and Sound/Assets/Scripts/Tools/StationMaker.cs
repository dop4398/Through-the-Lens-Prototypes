using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StationMaker : MonoBehaviour
{
    public bool active;

    [Header("Station Specs")]
    [SerializeField]
    [Range(2, 10)]
    public float TriggerSize;

    [Header("Tool Position")]
    [SerializeField]
    [Range(10, 1000)]
    public float xPos;
    [SerializeField]
    [Range(10, 1000)]
    public float yPos;

    [Header("Camera")]
    public Camera camera;

    [Header("GUI Skins")]
    public GUISkin normal;
    public GUISkin photo;

    private bool flag = false;
    private bool isPhotoMode = false;
    private bool capturePhoto = false;

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

    /// <summary>
    /// Code Source: https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
    /// </summary>
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/Resources/Screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void CapturePhoto()
    {
        capturePhoto = true;
    }

    void LateUpdate()
    {
        if (capturePhoto)
        {
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            camera.targetTexture = rt;
            int size = (int)(Screen.width * 500f / 1256f); // #### Custom Ratio ####
            Texture2D screenShot = new Texture2D(size, size, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect((Screen.width - size) / 2, (Screen.height - size) / 2, (Screen.width - size) / 2 + size, (Screen.height - size) / 2 + size), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(size, size);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            capturePhoto = false;
        }
    }

    private void OnGUI()
    {
        if (!active)
            return;

        GUI.skin = normal;

        // Make a background box
        GUI.Box(new Rect(xPos + 10, yPos + 10, 130, 110), "Cool Station Tool");

        //Unlock Cursor while holding down TAB
        if (CharacterComponents.instance)
        {
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
        }

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        isPhotoMode = GUI.Toggle(new Rect(xPos + 20, yPos + 30, 120, 20), isPhotoMode, "Screenshot Mode");

        // Make the second button.
        if (GUI.Button(new Rect(xPos + 20, yPos + 50, 100, 20), "Make Station"))
        {
            MakeStation();
        }

        if (GUI.Button(new Rect(xPos + 20, yPos + 70, 100, 20), "Capture photo"))
        {
            CapturePhoto();
        }

        if (GUI.Button(new Rect(xPos + 20, yPos + 90, 100, 20), "Close"))
        {
            Close();
        }

        GUI.skin = photo;

        if (isPhotoMode)
        {
            //Use golden radio to calculate size
            float size = Screen.width * (500f / 1256f);
            GUI.Box(new Rect(Screen.width / 2 - size / 2, Screen.height / 2 - size / 2, size, size), GUIContent.none);
        }
    }

    private void OnDrawGizmos()
    {
        if (!active)
            return;
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
        box.size = new Vector3(TriggerSize, 1, TriggerSize);
        trigger.AddComponent<StationTrigger>();

        //Set its parent to the new station also update station's trigger variable
        trigger.transform.parent = g.transform;
        trigger.transform.position = new Vector3(0f, -0.5f, 0f);
        station.trigger = trigger;

        //Snap Object
        GameObject snap = new GameObject("Snap");
        SphereCollider snapTrigger = snap.AddComponent<SphereCollider>();
        snapTrigger.isTrigger = true;
        snapTrigger.radius = 0.23f;
        snap.transform.parent = g.transform;


        //Set correct radius
        station.radius = 0.25f;

        //Read the current angle and position of player and record it into the new station
        station.rot.y = CharacterComponents.instance.gameObject.transform.rotation.eulerAngles.y;
        station.rot.x = CharacterComponents.instance.controller.playerCamera.transform.rotation.eulerAngles.x;
        g.transform.position = CharacterComponents.instance.controller.playerCamera.transform.position;

        station.vignetteScalar = 1f;
        station.isSingleUse = false;

        SaveAsPrefab(g, "Assets/Resources/Prefabs/Stations/");
    }

    public void SaveAsPrefab(GameObject obj, string path)
    {

        //Stores original object name
        string name = obj.name;

        //Path correction
        if (path[path.Length - 1] != '/')
        {
            path += '/';
        }

        //Generate new name
        string new_path = path + name + ".prefab";

        //Check for duplicate files
        int id = 1;
        while (System.IO.File.Exists(new_path))
        {
            new_path = path + name + "_" + id + ".prefab";
            id++;
        }
#if (UNITY_EDITOR)
        //Save object as prefab
        PrefabUtility.SaveAsPrefabAsset(obj, new_path);
#endif
    }

    public void Close()
    {
        if (CharacterComponents.instance)
            CharacterComponents.instance.controller.canLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        active = false;
    }

}
