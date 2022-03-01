using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    protected List<Material> materials;

    // Start is called before the first frame update
    protected void Start()
    {
        materials = new List<Material>();

        if (gameObject.GetComponent<MeshRenderer>() != null)
            materials.Add(gameObject.GetComponent<MeshRenderer>().material);
        else
        {
            MeshRenderer[] mr = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in mr)
            {
                materials.Add(renderer.material);
            }
        }
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    public void Interaction()
    {
        foreach (Material m in materials)
        {
            m.SetFloat("_Intensity", 0f);
        }
        CharacterComponents.instance.playerstate.SetState(PlayerState.inspecting);
        Inspector.instance.loader.LoadObject(gameObject);
        EventSystem.instance.ItemInspection();
    }
}
