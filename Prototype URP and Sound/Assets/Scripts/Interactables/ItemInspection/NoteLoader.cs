using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NoteLoader : MonoBehaviour
{
    string s;
    public TextAsset text;
    
    // Start is called before the first frame update
    void Start()
    {
        ReadString(text);
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

    public void ReadString(TextAsset txt)
    {
        GetComponent<Text>().text = txt.text;
    }
}
