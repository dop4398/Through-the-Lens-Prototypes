using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class Transitioner : MonoBehaviour
{
    public float transitionTime;
    public float waitTime;

    private Image black;

    private Tweener blackOut;
    private Tweener fadeIn;

    private float time;

    // Start is called before the first frame update
    void Start()
    {

        black = GetComponent<Image>();
        black.color = new Color(0, 0, 0, 1);

        time = transitionTime;

        blackOut = DOVirtual.Float(0f, 1f, time, x => black.color = new Color(0, 0, 0, x)).SetAutoKill(false).Pause().SetLink(gameObject);
        fadeIn = DOVirtual.Float(1f, 0f, time, x => black.color = new Color(0, 0, 0, x)).SetAutoKill(false).Pause().SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void DoRoomTransition()
    {
        float end = Time.time + waitTime + transitionTime;
        Indicator.instance.gameObject.SetActive(false);

        if (!blackOut.IsPlaying())
        {
            blackOut.Restart();
        }

        while(Time.time < end)
        {
            await Task.Yield();
        }

        Indicator.instance.gameObject.SetActive(true);
        fadeIn.Restart();
    }

    public async void DoFadeIn()
    {
        float end = Time.time + waitTime + transitionTime;
        Indicator.instance.gameObject.SetActive(false);

        Debug.Log("Fade in");
        while (Time.time < end)
        {
            await Task.Yield();
        }

        fadeIn.Restart();

        Indicator.instance.gameObject.SetActive(true);
    }

    public void DoFadeOut()
    {
        blackOut.Restart();
    }

    public async void DoIntroFadeIn()
    {
        float end = Time.time + transitionTime;
        Indicator.instance.gameObject.SetActive(false);

        Debug.Log("Intro Fade in");
        while (Time.time < end)
        {
            await Task.Yield();
        }

        fadeIn.Restart();
    }

    private void OnDestroy()
    {
        
    }

}
