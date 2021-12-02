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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStations();
    }

    private void UpdateStations()
    {
        if (stations.Count > 0)
        {
            float glow = 0f;
            float vig = 0;

            foreach (Station s in stations)
            {
                StationInfo info = s.CheckPlayer();
                if (info.glow > glow)
                    glow = info.glow;
                if (info.vignette > vig)
                    vig = info.vignette;
                if (info.success)
                {
                    if (PhotoController.instance.GetPhotoStatus())
                    {
                        //PhotoController.instance.ChangeState();
                        CharacterComponents.instance.heldPhoto.SwapPhotoContent();
                        s.ToggleState();
                    }
                }
            }

            this.glow = Mathf.Lerp(this.glow, glow, Time.deltaTime);
            this.vig = Mathf.Lerp(this.vig, vig, Time.deltaTime);

            PhotoController.instance.SetGlow(this.glow);
        }
        else
        {
            if (this.glow > 0f)
            {
                this.glow = Mathf.Lerp(this.glow, 0f, Time.deltaTime);

                if (Mathf.Abs(this.glow) < 0.01f)
                    this.glow = 0f;
            }
            PhotoController.instance.SetGlow(this.glow);
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
}
