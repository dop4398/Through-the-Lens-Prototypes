using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NoteLoader : MonoBehaviour
{
    string s;
    // Start is called before the first frame update
    void Start()
    {
        ReadString("Assets/Resources/TextFiles/file1.txt");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReadString(string path)
    {
        StreamReader reader = new StreamReader(path);
        GetComponent<Text>().text = reader.ReadToEnd();
        reader.Close();
    }
}
