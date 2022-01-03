using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Model;

public class main_menu : MonoBehaviour
{
    public GameObject helpScreen, statisticScreen, startScreen;
    public TMP_Text totalRacing, totalWins, totalScore;
    public List<TMP_Text> MapName = new List<TMP_Text>();
    public List<TMP_Text> Position = new List<TMP_Text>();
    public List<TMP_Text> Won = new List<TMP_Text>();
    public List<TMP_Text> XP = new List<TMP_Text>();

    public Model.FirebaseManager FBManager;

    // Start is called before the first frame update
    void Start()
    {
        if (FBManager.UserId == "")
        {
            FBManager.UserId = "NEKEOhAnJNUf96EfSkZCOHwphuJ3";
        }

    }

    // Update is called once per frame
    void Update() { }
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

        var scores = FBManager.GetUserScores();
        int limit = 3,  i = 0;
        foreach (Model.ScoreElement score in scores)
        {
            if (i == limit)
                break;

            Debug.Log(score.MapName);
            MapName[i].text = "" + score.MapName;
            Position[i].text = "" + score.Position;
            Won[i].text = "" + score.Won;
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
