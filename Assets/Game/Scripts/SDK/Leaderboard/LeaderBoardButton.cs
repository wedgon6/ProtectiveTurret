using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private GameObject _leaderboardPanel;

    public void OpenLeaderboard()
    {
        _leaderboardPanel.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.Fill();
#endif

    }
}
