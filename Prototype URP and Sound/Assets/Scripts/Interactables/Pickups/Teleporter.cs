using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    public InteractableType type;

    public Transform transform_enter;
    public Transform transform_exit;

    public List<GameObject> obj_enter;
    public List<GameObject> obj_exit;

    public bool requireKey;
    public int key_id;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interaction()
    {
        throw new System.NotImplementedException();
    }
}
