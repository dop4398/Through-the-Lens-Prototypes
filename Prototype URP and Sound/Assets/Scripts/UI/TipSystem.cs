using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class TipsInfo
{
    public string name;
    public GameObject tip;
    public float time;
    [HideInInspector]
    public Vector3 start;
    public Vector3 displacement;
}

public class TipSystem : MonoBehaviour
{
    public static TipSystem instance;

    public List<TipsInfo> tips;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initalize all tips
        foreach(TipsInfo t in tips)
        {
            t.start = t.tip.transform.position;
            t.tip.GetComponent<Image>().color = new Color(255,255,255,0);
            t.tip.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTip(string _name)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        //Activate it 
        t.tip.SetActive(true);

        //Transform Tween
        t.tip.transform.DOMove(t.tip.transform.position + t.displacement, t.time);

        //Alpha Tween
        t.tip.GetComponent<Image>().DOFade(255f, t.time);
    }

    public void ShowTip(string _name, float duration)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        //Activate it 
        t.tip.SetActive(true);

        //Transform Tween
        t.tip.transform.DOMove(t.tip.transform.position + t.displacement, t.time);

        //Alpha Tween
        t.tip.GetComponent<Image>().DOFade(255f, t.time);

        StartCoroutine(CloseTip(t, duration));
    }

    public void RemoveTip(string _name)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        CloseTip(t, 0f);
    }

    private IEnumerator CloseTip(TipsInfo t, float duration)
    {
        yield return new WaitForSeconds(duration);

        //Transform Tween
        t.tip.transform.DOMove(t.start, t.time);

        //Alpha Tween
        yield return t.tip.GetComponent<Image>().DOFade(255f, t.time).WaitForCompletion();

        //Deactivate it 
        t.tip.SetActive(true);
    }

}
