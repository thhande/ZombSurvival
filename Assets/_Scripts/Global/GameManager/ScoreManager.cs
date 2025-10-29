using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int highScore;
    public event System.Action OnHScoreChange;
    public event System.Action OnScoreChange;
    public void ResetScore()
    {
        score = 0;
    }
    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
            OnHScoreChange();
        }
        // playTime += Random.Range(2, 6);
        // OnTimePasses();
        OnScoreChange();
    }
}
