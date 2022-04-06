using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AlbumUI : MonoBehaviour
{
    public static AlbumUI instance;

    public List<GameObject> photo_slots;
    public List<Photo> photos;
    public GameObject parent;

    private int page = 1;

    [SerializeField]
    private int page_current = 0;
    private const int photo_per_page = 4;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        photos = new List<Photo>(4);
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            photo_slots.Add(parent.transform.GetChild(i).gameObject);
        }
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitUI()
    {
        UpdatePage();
        UpdateUI();
        photos = CharacterComponents.instance.album.album.Select(photo => new Photo(photo)).ToList();
    }

    public void UpdateUI()
    {

        for (int i = 0; i < photo_slots.Count; i++)
        {
            if (i + photo_per_page * page_current < CharacterComponents.instance.album.album.Count)
            {
                Texture2D tex = CharacterComponents.instance.album.album[i + photo_per_page * page_current].GetTexture_Current();
                photo_slots[i].GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                photo_slots[i].transform.GetChild(0).gameObject.SetActive(CharacterComponents.instance.album.IsInHand(i + photo_per_page * page_current));
                Debug.Log(i + " " + CharacterComponents.instance.album.IsInHand(i + photo_per_page * page_current));
            }
            else
            {
                photo_slots[i].GetComponent<Image>().sprite = null;
            }
        }
    }

    public void UpdatePage()
    {
        page = Mathf.CeilToInt(CharacterComponents.instance.album.album.Count / 4f);
    }

    public void ChangePage(int i)
    {
        page_current += i;
        page_current = Mathf.Clamp(page_current, 0, page - 1);

        UpdateUI();
    }

    public void ToggleHand(int index)
    {
        index = Mathf.Clamp(index, 0, 3);

        int target_index = index + photo_per_page * page_current;
        if (target_index >= CharacterComponents.instance.album.album.Count)
            return;

        if (CharacterComponents.instance.album.IsInHand(target_index))
        {
            CharacterComponents.instance.album.RemovePhotoFromHand(target_index);
        }
        else
        {
            CharacterComponents.instance.album.AddPhotoToHand(target_index);
        }

        UpdateUI();
    }
}
