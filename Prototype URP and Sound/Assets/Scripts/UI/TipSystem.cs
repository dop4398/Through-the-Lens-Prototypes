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
            t.tween_pos = t.tip.transform.DOMove(t.tip.transform.position + t.displacement, t.time).SetEase(Ease.OutQuart).SetAutoKill(false).Pause();
            t.tip.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
            t.tween_alpha = t.tip.GetComponent<Image>().DOFade(1f, t.time).SetEase(Ease.OutQuart).SetAutoKill(false).Pause();
            t.tip.GetComponent<Image>().color = new Color(1f,1f,1f,0);
            t.tip.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    ShowTip("WASD", 2f);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    ShowTip("LMB", 4f);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ShowTip("RMB");
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    RemoveTip("RMB");
        //}
    }

    //Display tip
    public void ShowTip(string _name)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        if (t.tween_alpha.IsPlaying() || t.tween_pos.IsPlaying())
            return;

        //Activate it 
        t.tip.SetActive(true);

        t.tween_pos.PlayForward();
        t.tween_alpha.PlayForward();
    }   

    //Display tip with duration
    public void ShowTip(string _name, float duration)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        if (t.tween_alpha.IsPlaying() || t.tween_pos.IsPlaying())
            return;

        //Activate it 
        t.tip.SetActive(true);

        t.tween_pos.PlayForward();
        t.tween_alpha.PlayForward();

        StartCoroutine(CloseTip(t, duration));
    }

    //Remove tip
    public void RemoveTip(string _name)
    {
        //Find the tip
        TipsInfo t = tips.Find(x => x.name == _name);

        StartCoroutine(CloseTip(t, 0f));
    }

    //Used to remove tip
    private IEnumerator CloseTip(TipsInfo t, float duration)
    {
        yield return new WaitForSeconds(duration);

        t.tween_pos.PlayBackwards();
        t.tween_alpha.PlayBackwards();
    }

}
