using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class main_menu : MonoBehaviour
{
    public GameObject helpScreen,statisticScreen,startScreen;
    public TMP_Text totalRacing,totalWins,totalScore;
    public List<TMP_Text> trackNo = new List<TMP_Text>();
    public List<TMP_Text> positionNo = new List<TMP_Text>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var i = 1;
        foreach(TMP_Text track in trackNo)
        {
            
            track.text=""+i;
            i++;
        }
        i = 10;
        foreach(TMP_Text position in positionNo)
        {
            position.text=""+i;
            i++;
        }
        totalRacing.text="10";
        totalWins.text="8";
        totalScore.text="250";
        
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
