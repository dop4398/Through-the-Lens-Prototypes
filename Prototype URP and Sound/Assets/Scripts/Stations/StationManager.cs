using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Station manager holds all stations in the game and will update all activeated stations. 
/// 1) Signal stations to switch state if needed
/// 2) Keep track of glow and vignette intensity and pass them to other classes
/// </summary>
public class StationManager : MonoBehaviour
{
    public static StationManager instance;
    SubScripts subtitles;
    [SerializeField]
    [Range(1, 10)]
    public float vig_speed = 1.0f;
    [SerializeField]
    [Range(1, 10)]
    public float glow_speed = 1.0f;

    private void Awake()
    {
        instance = this;
    }

    private List<Station> stations;
    private float glow;
    private float vig;

    // Start is called before the first frame update
    void Start()
    {
        stations = new List<Station>();
        subtitles = this.GetComponent<SubScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStations();
        //Debug.Log("stations.Count = " + stations.Count);
    }

    private void UpdateStations()
    {
        if (stations.Count > 0)
        {
            float glow = 0f, vig = 0, pulse = 0f;

            foreach (Station s in stations)
            {
                StationInfo info = s.CheckPlayer();

                if (info.glow > glow)
                    glow = info.glow;
                if (info.vignette > vig)
                    vig = info.vignette;
                if (info.pulse > pulse)
                    pulse = info.pulse;

                if (info.success)
                {
                    if (PhotoController.instance.GetPhotoStatus() && !CharacterComponents.instance.heldPhoto.swapHasTriggered && this.vig > 0.97f)
                    {
                        //PhotoController.instance.ChangeState();
                        CharacterComponents.instance.heldPhoto.swapHasTriggered = true; // Here to only swap once per focus
                        CharacterComponents.instance.heldPhoto.SwapPhotoContent();
                        CharacterComponents.instance.controller.LockCameraAndInput(s.rot, PhotoController.instance.GetTransitionTime());
                        PhotoController.instance.PlaySuccessParticle();
                        s.ToggleState();

                        //Insert station's id instead of 0 pls
                        subtitles.Run(int.Parse(s.id));

                    }
                }
            }

            //increase
            this.glow = Mathf.Lerp(this.glow, glow, Time.deltaTime * glow_speed);
            this.vig = Mathf.Lerp(this.vig, vig, Time.deltaTime * vig_speed);

            PhotoController.instance.SetGlow(this.glow);
            PPVController.instance.SetVignette(this.vig);

            if (pulse > 0)
            {
                PhotoController.instance.Pulse(true);
                PhotoController.instance.SetPulseSpeed(pulse);
            }
            else
            {
                PhotoController.instance.Pulse(false);
            }

        }
        else
        {
            //glow & vig decay
            if (this.glow > 0f)
            {
                this.glow = Mathf.Lerp(this.glow, 0f, Time.deltaTime);

                if (Mathf.Abs(this.glow) < 0.01f)
                    this.glow = 0f;
            }

            if (this.vig > 0f)
            {
                this.vig = Mathf.Lerp(this.vig, 0f, Time.deltaTime);

                if (Mathf.Abs(this.vig) < 0.01f)
                    this.vig = 0f;
            }

            PhotoController.instance.SetGlow(this.glow);
            PPVController.instance.SetVignette(this.vig);
        }
    }

    public void AddStation(Station s)
    {
        stations.Add(s);
    }

    public void RemoveStation(Station s)
    {
        stations.Remove(s);
    }

    public GameObject DetectedStationTrigger()
    {
        if(stations.Count > 0)
        {
            foreach(Station s in stations)
            {
                if(s.id == CharacterComponents.instance.heldPhoto.heldPhotoIndex.ToString())
                {
                    return s.trigger;
                }
            }
        }
        return null;
    }
}
