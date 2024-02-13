using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _currentScore = 0;

    public int CurrentScore => _currentScore;

    public void SetScoreData(int currentScore)
    {
        _currentScore = currentScore;
    }

    public void AddScore(int score)
    {
        _currentScore += score;
    }
}
