using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSurvey : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Application.OpenURL("https://rit.az1.qualtrics.com/jfe/form/SV_1G2WTvrmZyvESLI");
            Application.Quit();
        }
    }
}
