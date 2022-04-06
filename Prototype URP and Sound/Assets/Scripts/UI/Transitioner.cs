using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class Transitioner : MonoBehaviour
{
    public static Transitioner instance;
    public float transitionTime;
    public float waitTime;

    private Image black;

    private Tweener blackOut;
    private Tweener fadeIn;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        black = GetComponent<Image>();

        time = transitionTime;

        blackOut = DOVirtual.Float(0f, 1f, time, x => black.color = new Color(0, 0, 0, x)).SetAutoKill(false).Pause();
        fadeIn = DOVirtual.Float(1f, 0f, time, x => black.color = new Color(0, 0, 0, x)).SetAutoKill(false).Pause();

        DoFadeIn();
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


        while (Time.time < end)
        {
            await Task.Yield();
        }

        fadeIn.Restart();
        end = Time.time + waitTime + transitionTime;

        while (Time.time < end)
        {
            await Task.Yield();
        }

        Indicator.instance.gameObject.SetActive(true);

    }

    public async void DoFadeOut()
    {
        blackOut.Restart();
    }

}
