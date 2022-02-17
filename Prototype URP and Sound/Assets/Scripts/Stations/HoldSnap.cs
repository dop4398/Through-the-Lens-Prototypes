using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoldSnap : MonoBehaviour
{
    public Vector3 target;
    public bool isActive;

    public float snapTime;

    private Tweener tweenX;
    private Tweener tweenZ;

    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {

            if (PlayerInput.playerInput.focusPhoto)
                Snap();

            if (PlayerInput.playerInput.unfocusPhoto)
                Return();

        }
    }

    public void Snap()
    {
        tweenX = CharacterComponents.instance.transform.DOMoveX(transform.position.x, snapTime);
        tweenZ = CharacterComponents.instance.transform.DOMoveZ(transform.position.z, snapTime);

        origin = CharacterComponents.instance.transform.position;

        tweenX.OnPlay(() => { SetPController(false); });
    }

    public void Return()
    {
        tweenX.Pause();
        tweenZ.Pause();

        SetPController(true);
    }

    public void SetPController(bool b)
    {
        CharacterComponents.instance.controller.GetComponent<CharacterController>().enabled = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
        }
    }
}
