using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string AnonymousName = "Anonymous";
    private const string LeaderboardName = "Leaderboard";

    [SerializeField] private ViewLeaderboard _leaderboardView;

    private readonly List<DataPlayer> _leaderboardPlayers = new List<DataPlayer>();

    public void SetPlayer(int score)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result.score < score)
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, score);
        });
#endif
    }

    public void Fill()
    {
        _leaderboardPlayers.Clear();
        Debug.Log("ClearPlayer Data");

        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, result  =>
        {
            foreach (var entry in result.entries)
            {
                var name = entry.player.publicName;
                var rank = entry.rank;
                var score = entry.score;

                if (string.IsNullOrEmpty(name))
                {
                    name = AnonymousName;
                }

                _leaderboardPlayers.Add(new DataPlayer(name, rank, score));
            }
            Debug.Log(_leaderboardPlayers.Count+"___LeanerboardPlayerCount");
            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        });
    }
}
