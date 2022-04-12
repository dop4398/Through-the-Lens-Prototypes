using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoHandler : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += AfterVideo;
    }
    public void AfterVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene("ApartmentRework");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
