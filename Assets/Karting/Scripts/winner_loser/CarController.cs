using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isAI;
    public int currentTarget;
    private Vector3 targetPoint;
    public float aiAccelerateSpeed = 1f, aiTurnSpeed = .8f, aiReachRange = 5f, aiPointVariance = 3f;
    private float aiSpeedInput;
    private int nextCheckpoint;
    public int currentLap;
    public float lapTime, bestLapTime;
    public static CarController instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (isAI)
        {
            targetPoint = race_manger.ins.allcheckpoints[currentTarget].transform.position;
            RandomisAITarget();
        }
    }
    void Update()
    {

        lapTime += Time.deltaTime;

        var ts = System.TimeSpan.FromSeconds(lapTime);
        UImanager.instance.currentLapText.text = string.Format("{0:00}m{1:00}.{2:000}s", ts.Minutes, ts.Seconds, ts.Milliseconds);
        if (isAI)
        {
            targetPoint.y = transform.position.y;
            if (Vector3.Distance(transform.position, targetPoint) < aiReachRange)
            {
                currentTarget++;
                if (currentTarget >= race_manger.ins.allcheckpoints.Length)
                {
                    currentTarget = 0;
                }
                targetPoint = race_manger.ins.allcheckpoints[currentTarget].transform.position;
                RandomisAITarget();

            }
        }
        
    }
    public void CheckpointHit(int cpNumber)
    {
        Debug.Log(cpNumber);
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
            }
            

        }
        lapTime = 0f;
    }
    public void RandomisAITarget()
    {
        targetPoint += new Vector3(Random.Range(-aiPointVariance, aiPointVariance), 0f, Random.Range(-aiPointVariance, aiPointVariance));
    }
}
