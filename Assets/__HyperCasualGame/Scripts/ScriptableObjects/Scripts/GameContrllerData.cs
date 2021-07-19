using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMnagerData", menuName = "GameManger / GameData", order = 1)]
public class GameContrllerData : ScriptableObject
{
    public bool isGameWin = false;
    public bool isGameLose = false;
    public int Score = 0;


    public void RestData()
    {
        isGameWin = false;
        isGameLose = false;
        Score = 0;
    }
}
