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
    [SerializeField]
    [Range(0.1f,2f)]
    public float time;
    [HideInInspector]
    public Vector3 start;
    public Vector3 displacement;
    public Tweener tween_pos;
    public Tweener tween_alpha;
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
        foreach (TipsInfo t in tips)
        {
            t.start = t.tip.transform.position;
            t.tween_pos = t.tip.transform.DOMove(t.tip.transform.position + t.displacement, t.time).SetAutoKill(false).Pause();
            t.tip.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
            t.tween_alpha = t.tip.GetComponent<Image>().DOFade(1f, t.time).SetAutoKill(false).Pause();
            t.tip.GetComponent<Image>().color = new Color(1f,1f,1f,0);
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

        if (t.tween_alpha.IsPlaying() || t.tween_pos.IsPlaying())
            return;

        //Activate it 
        t.tip.SetActive(true);

        t.tween_pos.Play();
        t.tween_alpha.Play();
    }   

    public void ShowTip(string _name, float duration)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        if (t.tween_alpha.IsPlaying() || t.tween_pos.IsPlaying())
            return;

        //Activate it 
        t.tip.SetActive(true);

        t.tween_pos.Play();
        t.tween_alpha.Play();

        StartCoroutine(CloseTip(t, duration));
    }

    public void RemoveTip(string _name)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        StartCoroutine(CloseTip(t, 0f));
    }

    private IEnumerator CloseTip(TipsInfo t, float duration)
    {
        yield return new WaitForSeconds(duration);

        t.tween_pos.PlayBackwards();
        t.tween_alpha.PlayBackwards();

        t.tween_alpha.onComplete += () =>
        {
            //Deactivate it 
            t.tip.SetActive(false);
        };
        //Alpha Tween
        yield return t.tween_alpha.WaitForCompletion();

        
    }

}
