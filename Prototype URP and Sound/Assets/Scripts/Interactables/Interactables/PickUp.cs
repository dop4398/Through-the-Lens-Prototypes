using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUp : Interactable, IInteractable
{
    [SerializeField]
    protected List<Material> materials;
    public bool usable;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        materials = new List<Material>();

        type = InteractableType.Pickup;

        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            materials.Add(gameObject.GetComponent<MeshRenderer>().material);

        }
        else
        {
            MeshRenderer[] mr = gameObject.GetComponentsInChildren<MeshRenderer>();

            if (mr.Length != 0)
            {
            }

            foreach (MeshRenderer renderer in mr)
            {
                materials.Add(renderer.material);
            }
        }
    }

    public virtual void Interaction()
    {
        RemoveHighlight();
        Inspector.instance.loader.LoadObject(gameObject);
    }

    public virtual void Use()
    {

    }

    public virtual void RemoveHighlight()
    {
        foreach (Material m in materials)
        {
            m.SetFloat("_Intensity", 0f);
        }
    }
}
