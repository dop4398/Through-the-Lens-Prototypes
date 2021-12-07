using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipRemover : MonoBehaviour
{

    public List<KeyCode> inputs;
    public string name;
    public bool destorySelf;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (CheckPlayerInput())
            {
                Debug.Log("Input");
                StartCoroutine(RemoveTip());
            }
        }
    }

    bool CheckPlayerInput()
    {
        foreach(KeyCode k in inputs)
        {
            if (Input.GetKeyDown(k))
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator RemoveTip()
    {
        yield return new WaitForSeconds(1f);

        TipSystem.instance.RemoveTip(name);

        if (destorySelf)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        flag = true;
    }
}
