using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string AnonymousName = "Anonymous";
    private const string LeaderboardName = "Leaderboard";
    private readonly List<DataPlayer> _leaderboardPlayers = new();

    [SerializeField] private ViewLeaderboard _leaderboardView;

    public void SetPlayer(int score)
    {
        Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, score);
    }

    public void Fill()
    {
        _leaderboardPlayers.Clear();

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

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        });
    }
}
