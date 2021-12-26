using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

// [Serializable]
public class ScoreElement
{
    public bool Won;
    public int Position;
    public string MapName;
    public int XP;

    public ScoreElement(bool _won, int _pos, string _mapName, int _xp)
    {
        Position = _pos;
        MapName = _mapName;
        Won = _won;
        XP = _xp;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }


}