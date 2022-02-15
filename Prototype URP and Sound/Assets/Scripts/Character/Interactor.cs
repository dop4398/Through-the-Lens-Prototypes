using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask layer = 7;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1f, layer))
        {
            if (PlayerInput.playerInput.interact)
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interaction();
            }
        }

    }
}
