using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUp : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected List<Material> materials;
    public InteractableType type = InteractableType.Pickup;
    public bool usable;

    // Start is called before the first frame update
    protected void Start()
    {
        materials = new List<Material>();

        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            Debug.Log("Have MR");
            materials.Add(gameObject.GetComponent<MeshRenderer>().material);

        }
        else
        {
            MeshRenderer[] mr = gameObject.GetComponentsInChildren<MeshRenderer>();

            if (mr.Length != 0)
            {
                Debug.Log("Have multiple MR");
            }

            foreach (MeshRenderer renderer in mr)
            {
                materials.Add(renderer.material);
            }
        }
    }

    public virtual void Interaction()
    {
        foreach (Material m in materials)
        {
            m.SetFloat("_Intensity", 0f);
        }
        CharacterComponents.instance.playerstate.SetState(PlayerState.inspecting);
        Inspector.instance.loader.LoadObject(gameObject);
        EventSystem.instance.ItemInspection();
    }

    public virtual void Use()
    {
        if (!usable)
            return;
    }
}
