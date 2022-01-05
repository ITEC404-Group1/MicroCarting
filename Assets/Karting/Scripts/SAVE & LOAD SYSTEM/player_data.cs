using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class player_data 
{
    public int TotalGame;
    public int score;
    public int totalWin;

    public player_data (player player)
    {
        TotalGame = player.TotalGame;
        score = player.score;
        totalWin = player.totalWin;

    }

}
