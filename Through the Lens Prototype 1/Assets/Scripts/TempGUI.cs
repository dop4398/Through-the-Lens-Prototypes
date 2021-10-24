using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialType
{
    WASDMOUSE,
    HOLDPHOTO,
    SWAPPHOTO,
    PICKUP,
    LINEUP,
    LOCKED,
    GATHER
}
public class TempGUI : MonoBehaviour
{

    public static TempGUI gui;

    public GameObject wasd;
    public GameObject hold;
    public GameObject swap;
    public GameObject pick;
    public GameObject lineUp;
    public GameObject locked;
    public GameObject gather;

    [SerializeField]
    [Range(0.1f,2f)]
    private float tipSpeed_appear;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float tipSpeed_disappear;

    protected Action<object> OnTweenFinished;

    private void Awake()
    {
        gui = this;
        OnTweenFinished = TweenFinished;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (swap.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            TurnOffTutorial(TutorialType.SWAPPHOTO);
        }
        else if (hold.activeInHierarchy && Input.GetKeyDown(KeyCode.Mouse1))
        {
            TurnOffTutorial(TutorialType.HOLDPHOTO);
            TurnOnTutorial(TutorialType.LINEUP);
        }
    }

    private void OnGUI()
    {

    }

    public void SetName(string s)
    {
        name = s;
    }

    public void TurnOnTutorial(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.HOLDPHOTO:
                ShowTips(hold);
                break;
            case TutorialType.PICKUP:
                ShowTips(pick);
                break;
            case TutorialType.SWAPPHOTO:
                ShowTips(swap);
                break;
            case TutorialType.WASDMOUSE:
                ShowTips(wasd);
                break;
            case TutorialType.LINEUP:
                ShowTips(lineUp);
                break;
            case TutorialType.LOCKED:
                ShowTips(locked);
                break;
            case TutorialType.GATHER:
                ShowTips(gather);
                break;
        }
    }

    public void TurnOffTutorial(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.HOLDPHOTO:
                RemoveTips(hold);
                break;
            case TutorialType.PICKUP:
                RemoveTips(pick);
                break;
            case TutorialType.SWAPPHOTO:
                RemoveTips(swap);
                break;
            case TutorialType.WASDMOUSE:
                RemoveTips(wasd);
                break;
            case TutorialType.LINEUP:
                RemoveTips(lineUp);
                break;
            case TutorialType.LOCKED:
                RemoveTips(locked);
                break;
            case TutorialType.GATHER:
                RemoveTips(gather);
                break;
        }
    }

    protected void ShowTips(GameObject g)
    {
        if (g.GetComponent<RectTransform>().anchoredPosition != Vector2.zero)
            return;

        g.SetActive(true);
        g.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        LeanTween.alpha(g.GetComponent<RectTransform>(), 1f, tipSpeed_appear).setEaseInOutQuad();
        LeanTween.moveLocalY(g, 50, tipSpeed_appear).setEaseInOutQuad();
    }

    protected void RemoveTips(GameObject g)
    {
        LeanTween.moveLocalY(g, 0f, tipSpeed_disappear).setEaseInOutQuad();
        LeanTween.alpha(g.GetComponent<RectTransform>(), 0f, tipSpeed_disappear).setEaseInOutQuad().setOnComplete(TweenFinished, g);
    }

    protected void TweenFinished(object obj)
    {
        GameObject g = (GameObject)obj;
        g.SetActive(false);
    }
}
