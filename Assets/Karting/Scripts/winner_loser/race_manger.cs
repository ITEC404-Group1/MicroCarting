using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class race_manger : MonoBehaviour
{
    public GameObject resultScreen;
    public TMP_Text completed,position,msg;
    public bool isStrating;
    public Transform player;
    public float timeBetweenStartCount = 1f;
    private float startCounter;
    public int counterdownCurrent = 3;
    private float dist_player;
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

        isStrating = true;
        startCounter = timeBetweenStartCount;
    }

    // Update is called once per frame
    void Update()
    {
        
                
        if (isStrating)
        {
            startCounter -= Time.deltaTime;
            if (startCounter <= 0)
            {
                counterdownCurrent--;
                
                startCounter = timeBetweenStartCount;
                if (counterdownCurrent == 0)
                {
                    isStrating = false;
                }
            }
        }
        else
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
                    }
                    else if (aiCar.currentLap == playerCar.currentLap)
                    {
                        if (aiCar.nextCheckpoint > playerCar.nextCheckpoint)
                        {
                            playerPosition++;
                        }
                        else if (aiCar.nextCheckpoint == playerCar.nextCheckpoint)
                        {
                            if (player)
                            {
                                dist_player = Vector3.Distance(player.position, allcheckpoints[playerCar.nextCheckpoint].transform.position);
                            }
                            if (Vector3.Distance(aiCar.transform.position, allcheckpoints[aiCar.nextCheckpoint].transform.position) < dist_player)
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
}
