using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class race_manger : MonoBehaviour
{
    public CarController playerCar;
    public List<CarController> allAICars = new List<CarController>();
    public static race_manger ins;
    public int playerPosition;
    public float timeBetweenPosCheck = .2f;
    private float posChkCounter;
    public check_point[] allcheckpoints;
    public int totalLaps;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        for (int i = 0; i < allcheckpoints.Length; i++)
        {
            allcheckpoints[i].cpNumber = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        posChkCounter -= Time.deltaTime;
        if (posChkCounter <= 0)
        {
            playerPosition = 1;
            foreach (CarController aiCar in allAICars)
            {
                if (aiCar.currentLap > playerCar.currentLap)
                {
                    playerPosition++;
                }else if (aiCar.currentLap == playerCar.currentLap)
                {
                    if (aiCar.nextCheckpoint > playerCar.nextCheckpoint)
                    {
                        playerPosition++;
                    }
                    else if (aiCar.nextCheckpoint == playerCar.nextCheckpoint)
                    {
                        if (Vector3.Distance(aiCar.transform.position, allcheckpoints[aiCar.nextCheckpoint].transform.position) < Vector3.Distance(aiCar.transform.position, allcheckpoints[aiCar.nextCheckpoint].transform.position))
                        {
                            playerPosition++;
                        }
                    }
                }
            }
            posChkCounter = timeBetweenPosCheck;
        }

    }
}
