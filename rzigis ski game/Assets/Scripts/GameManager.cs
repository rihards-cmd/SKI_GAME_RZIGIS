using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DateTime raceStart;
    private TimeSpan raceTime;
    private bool racing;
    public delegate void TimerEvent();

    public void OnEnable()
    {
        StartGate.StartRace += OnRaceStart;
        FinishGate.FinishRace += OnRaceFinish;
    }

    void OnRaceStart()
    {
        Debug.Log("Started race");
        racing = true;
        raceStart = DateTime.Now;
    }

    void OnRaceFinish()
    {
        Debug.Log("Finished race");
        racing = false;
    }

    private void Update()
    {
        if(racing)
        {
            raceTime = DateTime.Now - raceStart;
            Debug.Log("Race time:" + raceTime);
        }
    }
}
