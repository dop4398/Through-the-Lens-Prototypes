using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateRattleOneOff : MonoBehaviour
{
    private FMOD.Studio.EventInstance rattle;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void PlayGateRattle()
    {
        rattle = FMODUnity.RuntimeManager.CreateInstance("event:/Interactions/Gate Rattle");
        rattle.start();
        rattle.release();
    }
}
