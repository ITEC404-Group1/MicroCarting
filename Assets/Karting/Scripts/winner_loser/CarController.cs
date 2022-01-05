using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    
    public bool isAI;
    private float aiSpeedInput;
    public int nextCheckpoint;
    public int currentLap;
    public float lapTime, bestLapTime;
 public string mapName;
    public static CarController instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
          Scene scene = SceneManager.GetActiveScene();
          mapName = scene.name;

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
            race_manger.ins.resultScreen.SetActive(true);
            if(race_manger.ins.playerPosition == 1)
            {
                player.instance.TotalGame ++;
                player.instance.totalWin ++;
                player.instance.score +=20;
                player.instance.savePlayer();
                race_manger.ins.position.text="YOU FINISHED 1ST";
                race_manger.ins.msg.text="congratulations";
                Debug.Log("winner");
                GameFlowManager.instance.EndGame(true);
            }else
            {  
                player.instance.TotalGame ++;
                if(race_manger.ins.playerPosition == 2)
                {
                    player.instance.score +=10;
                race_manger.ins.position.text="YOU FINISHED 2ND";
                race_manger.ins.msg.text="Try to be better";
                }else{
                    player.instance.score +=5;
                }
                player.instance.savePlayer();
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
