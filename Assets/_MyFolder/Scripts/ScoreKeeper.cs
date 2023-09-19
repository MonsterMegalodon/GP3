using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "High")]

public class ScoreKeeper : ScriptableObject
{
    public int highScore;

    public void SetScore(int score)
    {
        highScore = score;
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
