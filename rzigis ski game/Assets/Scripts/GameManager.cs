using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private DateTime raceStart;
    private TimeSpan raceTime;
    private bool racing;
    public delegate void TimerEvent();

    [SerializeField] private int addPenaltyTime = 3;
    private TimeSpan penaltyTime;

    [SerializeField] private TMP_Text raceTimeText, bestTimeText;

    [SerializeField] private string BestTimeKey = "LVL1 best time";
    private TimeSpan bestTime;


    public void OnEnable()
    {
        StartGate.StartRace += OnRaceStart;
        FinishGate.FinishRace += OnRaceFinish;
        SlalomFlag.RacePenalty += AddRacePenalty;
    }
    public void OnDisable()
    {
        StartGate.StartRace -= OnRaceStart;
        FinishGate.FinishRace -= OnRaceFinish;
        SlalomFlag.RacePenalty -= AddRacePenalty;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(BestTimeKey))
        {
            int bestTimeTicks = PlayerPrefs.GetInt(BestTimeKey);
            bestTime = new TimeSpan(bestTimeTicks);
            Debug.Log("best time:" +  bestTime);
            bestTimeText.text = "BEST TIME:" + bestTime.ToString("ss\\:ff");

        }
        else
        {
            bestTime = new TimeSpan(long.MaxValue);
            bestTimeText.text = "BEST TIME: ---";
        }
    }

    void AddRacePenalty()
    {
        penaltyTime += new TimeSpan(0, 0, addPenaltyTime);
    }
    void OnRaceStart()
    {
        Debug.Log("Started race");
        racing = true;
        raceStart = DateTime.Now;
    }

    void OnRaceFinish()
    {
        float raceTimeF = (float)raceTime.TotalMilliseconds / 1000f;
        GameData.Instance.AddTime(raceTimeF);
        Debug.Log("Finished race");
        racing = false;
        if (raceTime < bestTime)
        {
            bestTime = raceTime;
            bestTimeText.text = "BEST TIME:" + bestTime.ToString("ss\\:ff");
            bestTimeText.color = Color.yellow;
            PlayerPrefs.SetInt(BestTimeKey, (int) bestTime.Ticks);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        if(racing)
            raceTime = DateTime.Now - raceStart + penaltyTime;
            //Debug.Log("Race time:" + raceTime);
        raceTimeText.text = "TIME:" + raceTime.ToString("ss\\:ff");
    }
}
