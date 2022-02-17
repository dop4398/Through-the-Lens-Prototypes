using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Application.OpenURL("https://rit.az1.qualtrics.com/jfe/form/SV_a4tJKLVLepw8sFE");
            Application.Quit();
        }
    }
}
