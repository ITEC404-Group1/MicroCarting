using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int TotalGame ;
    public int score ;
    public int totalWin ;
    public string mapName;
    public static player instance;
    private void Awake()
    {
        instance = this;
    }
     void Start()
    {
        player.instance.loadPlayer();
    }
    public void savePlayer()
    {
        saveSystem.savePlayer(this);
    }
    public void loadPlayer ()
    {
  player_data data =  saveSystem.loadPlayer();
  TotalGame = data.TotalGame;
  score = data.score;
  totalWin = data.totalWin;
  mapName = data.mapName;
    }

    public void resetButton()
    {
        score=0;
        totalWin=0;
        TotalGame=0;
        savePlayer();
        loadPlayer();
    }
}


