using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locus : MonoBehaviour
{
    public enum State
    {
        present,
        past
    }
    public int id;

    public List<GameObject> past;
    public List<GameObject> present;
    private Vector3 pos;
    private Vector3 rot;
    public State state;
    public bool isSingleUse;

    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
            ToggleState();
    }

    private void Init()
    {
        pos = this.pos;
        rot = this.rot;
        state = this.state;
        isSingleUse = this.isSingleUse;
    }

    private void ToggleState()
    {
        if(state == State.past)
        {
            SwitchState(State.present);
        }
        else
        {
            SwitchState(State.past);
        }
    }

    private void SwitchState(State s)
    {
        switch (s)
        {
            case State.past:
                if(present.Count > 0)
                {
                    foreach (GameObject g in present)
                        g.SetActive(false);
                }
                if(past.Count > 0)
                {
                    foreach (GameObject g in past)
                        g.SetActive(true);
                }
                break;
            case State.present:
                if (present.Count > 0)
                {
                    foreach (GameObject g in present)
                        g.SetActive(true);
                }
                if (past.Count > 0)
                {
                    foreach (GameObject g in past)
                        g.SetActive(false);
                }
                break;
        }

        state = s;

        if (isSingleUse)
            gameObject.SetActive(false);
    }

}
