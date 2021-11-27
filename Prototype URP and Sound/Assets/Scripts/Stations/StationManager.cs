using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Station manager holds all stations in the game and will check player status when needed, will also signle stations to switch state if needed
/// </summary>
public class StationManager : MonoBehaviour
{
    public static StationManager instance;

    private void Awake()
    {
        instance = this;
    }

    private List<Station> stations;

    // Start is called before the first frame update
    void Start()
    {
        stations = new List<Station>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
