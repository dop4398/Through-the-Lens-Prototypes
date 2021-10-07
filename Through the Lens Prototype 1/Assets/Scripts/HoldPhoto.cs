using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldPhoto : MonoBehaviour
{
    public static HoldPhoto instance;

    public bool isHolding;

    public GameObject photo;
    public GameObject photo_hold;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isHolding = false;
        photo_hold.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isHolding = true;

            photo_hold.SetActive(true);
            photo.SetActive(false);

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isHolding = false;

            photo_hold.SetActive(false);
            photo.SetActive(true);
        }
    }

    public void SetSprite(Sprite s)
    {
        photo.GetComponent<Image>().sprite = s;
        photo_hold.GetComponent<Image>().sprite = s;
    }
}
