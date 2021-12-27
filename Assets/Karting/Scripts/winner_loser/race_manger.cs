using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class race_manger : MonoBehaviour
{
    public static race_manger ins;
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

    }
}
