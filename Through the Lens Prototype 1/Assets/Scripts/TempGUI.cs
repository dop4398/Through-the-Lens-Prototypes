using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGUI : MonoBehaviour
{
    private string name;
    private GUIStyle style;

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

        GUI.Box(new Rect(20, 20, 120, 30), "Item: " + name, style);
    }

    public void SetName(string s)
    {
        name = s;
    }
}
