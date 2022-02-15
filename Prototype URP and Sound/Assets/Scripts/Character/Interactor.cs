using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask layer = 7;
    public float radius = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, radius, layer))
        {
            if (PlayerInput.playerInput.interact)
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interaction();
            }
        }

    }
}
