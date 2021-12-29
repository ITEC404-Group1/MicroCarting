using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isAI;
    private float aiSpeedInput;
    public int nextCheckpoint;
    public int currentLap;
    public float lapTime, bestLapTime;
    public static CarController instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
     
    }
    void FixedUpdate()
    {
        if(!race_manger.ins.isStrating)
        {

        lapTime += Time.deltaTime;

        var ts = System.TimeSpan.FromSeconds(lapTime);
        UImanager.instance.currentLapText.text = string.Format("{0:00}m{1:00}.{2:000}s", ts.Minutes, ts.Seconds, ts.Milliseconds);
        UImanager.instance.postionPlayer.text = race_manger.ins.playerPosition + "/" + 3;
        }
        
    }
    public void CheckpointHit(int cpNumber)
    {
        //Debug.Log(cpNumber);
        if (cpNumber == nextCheckpoint)
        {
            nextCheckpoint++;
            if (nextCheckpoint == race_manger.ins.allcheckpoints.Length)
            {
                nextCheckpoint = 0;
                lapCompleted();
            }
        }
    }
    public void lapCompleted()
    {
        currentLap++;

        if (lapTime < bestLapTime || bestLapTime == 0)
        {
            bestLapTime = lapTime;
        }
        if (!isAI)
        {
            if (race_manger.ins.totalLaps+1>=currentLap)
            {
               var ts = System.TimeSpan.FromSeconds(bestLapTime);
            UImanager.instance.bestLapTimeText.text = string.Format("{0:00}m{1:00}.{2:000}s", ts.Minutes, ts.Seconds, ts.Milliseconds);
            if (race_manger.ins.totalLaps>=currentLap)
            {
            UImanager.instance.lapCounterText.text = currentLap + "/" + race_manger.ins.totalLaps;
              
            }
            if(race_manger.ins.totalLaps+1<=currentLap)
        {
            if(race_manger.ins.playerPosition == 1)
            {
                Debug.Log("winner");
                GameFlowManager.instance.EndGame(true);
            }else
            {  
               Debug.Log("loser")  ;
               GameFlowManager.instance.EndGame(false);
            }
        }
            }
            
            

        }
        lapTime = 0f;
    }
    // void RestToTrack()
    // {
    //     int pointToGoTo = nextCheckpoint -1;
    
    //     if(pointToGoTo < 0)
    //     {
    //         pointToGoTo = race_manger.ins.allcheckpoints[pointToGoTo].transform.position;
    //         theRB
    //     }
    // }
}
