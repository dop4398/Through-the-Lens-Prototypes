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

        if (Physics.Raycast(CharacterComponents.instance.controller.playerCamera.transform.position, CharacterComponents.instance.controller.playerCamera.transform.forward, out hit, radius, layer))
        {
            Indicator.instance.SetSprite(hit.collider.gameObject);

            if (PlayerInput.playerInput.interact && CharacterComponents.instance.playerstate.GetState() == PlayerState.normal)
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interaction();
            }
        }
        else
        {
            Indicator.instance.SetSprite();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(CharacterComponents.instance.controller.playerCamera.transform.position,
        //    CharacterComponents.instance.controller.playerCamera.transform.position + CharacterComponents.instance.controller.playerCamera.transform.forward * radius);
    }
}
