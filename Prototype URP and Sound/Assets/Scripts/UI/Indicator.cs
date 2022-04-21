using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public static Indicator instance;

    private Image image;

    public Sprite Pickup;

    public Sprite Teleporter;
    public Sprite Lock;
    public Sprite Key;

    public Sprite Examinable;

    public Sprite Nonpickup;

    public Sprite dot;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        image = GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }

    public void SetSprite(GameObject obj)
    {
        InteractableType type = obj.GetComponent<Interactable>().GetInteractionType();

        switch (type)
        {
            case InteractableType.Pickup:
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                image.sprite = Pickup;
                break;
            case InteractableType.Teleporter:
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                Teleporter teleporter = obj.GetComponent<Teleporter>();
                if (!teleporter.isLocked)
                {
                    image.sprite = Teleporter;
                }
                else
                {
                    if (teleporter.CanUnlock())
                        image.sprite = Key;
                    else
                        image.sprite = Lock;
                }
                break;
            case InteractableType.Examinable:
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                image.sprite = Examinable;
                break;
            case InteractableType.NonPickup:
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                image.sprite = Nonpickup;
                break;
            case InteractableType.None:
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
                image.sprite = dot;
                break;
            default:
                break;
        }
    }

    public void SetSprite()
    {
        if(image == null)
        {
            image = GetComponentInChildren<Image>();
        }
        image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        image.sprite = dot;
    }
}
