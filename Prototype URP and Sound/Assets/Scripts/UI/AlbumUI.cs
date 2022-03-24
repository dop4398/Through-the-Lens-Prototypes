using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumUI : MonoBehaviour
{
    public List<GameObject> photos;

    private int page = 1;
    private int page_current = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            photos.Add(gameObject.transform.GetChild(i).gameObject);
        }

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUI()
    {
        for (int i = 0; i < photos.Count; i++)
        {
            Texture2D tex = CharacterComponents.instance.album.album[i].GetTexture_Current();
            photos[i].GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        }
    }

    public void UpdateUI()
    {
    }

    public void ChangePage(int i)
    {
        page_current += i;
        page_current = Mathf.Clamp(page_current, 1, page);
    }
}
