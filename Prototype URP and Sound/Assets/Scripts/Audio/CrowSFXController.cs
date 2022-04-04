using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CrowSFXController : MonoBehaviour
{
    public GameObject[] crows;

    // make a variable timer by adding a random number between 0 and 20 to a timer cap each cycle
    // call one of the emitters at random whenever that timer ticks
    public float timerBase = 5;
    public float timerMin = 0;
    public float timerMax = 5;
    public float additionalTime = 0;
    public float timer = 0;


    void Start()
    {
        additionalTime = Random.Range(timerMin, timerMax);

        foreach (GameObject c in crows)
        {
            c.GetComponent<StudioEventEmitter>().SetParameter("PlayerIsOutside", 1);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timerBase + additionalTime)
        {
            // reset the timer
            additionalTime = Random.Range(timerMin, timerMax);
            timer = 0;

            // Call a random emitter from the array
            crows[Random.Range(0, crows.Length)].GetComponent<StudioEventEmitter>().Play();
        }
    }
}
