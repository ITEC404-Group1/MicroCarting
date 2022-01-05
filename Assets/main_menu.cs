using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Model;

public class main_menu : MonoBehaviour
{
    public GameObject helpScreen, statisticScreen, startScreen;
    public TMP_Text totalRacing , totalWins, totalScore,mapName;
    public List<TMP_Text> MapName = new List<TMP_Text>();
    public List<TMP_Text> Position = new List<TMP_Text>();
    public List<TMP_Text> XP = new List<TMP_Text>();

    private int i = 0;
    public Model.FirebaseManager FBManager;

    // Start is called before the first frame update
    void Start()
    {
        player.instance.loadPlayer();
        totalRacing.text="" + player.instance.TotalGame;
        totalScore.text="" + player.instance.score;
       totalWins.text="" + player.instance.totalWin;
        if (FBManager.UserId == "")
        {
            FBManager.UserId = "NEKEOhAnJNUf96EfSkZCOHwphuJ3";
        }
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.instance.loadPlayer();
        totalRacing.text="" + player.instance.TotalGame;
        totalScore.text="" + player.instance.score;
       totalWins.text="" + player.instance.totalWin;
        mapName.text=player.instance.mapName;
          
    }
    public void openHelp()
    {
        helpScreen.SetActive(true);
    }
    public void closeHelp()
    {
        helpScreen.SetActive(false);
    }
    public void openStatistic()
    {   
        statisticScreen.SetActive(true);
    }

    public void generateTable()
    {
        var scores = FBManager.GetUserScores();
        int limit = 3, i = 0;
        foreach (Model.ScoreElement score in scores)
        {
            if (i == limit)
                break;

            //Debug.Log(score.MapName);
            MapName[i].text = "" + score.MapName;
            Position[i].text = "" + score.Position;
            XP[i].text = "" + score.XP;

            i++;
        }
    }
    public void closeStatistic()
    {
        statisticScreen.SetActive(false);
    }

    public void openstartScreen()
    {
        startScreen.SetActive(true);
    }
    public void closestartScreen()
    {
        startScreen.SetActive(false);
    }
    
}
