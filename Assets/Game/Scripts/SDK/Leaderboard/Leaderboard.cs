using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Leaderboard : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string AnonymousRu = "??????";
    private const string AnonymousEn = "Anonymous";
    private const string AnonymousTr = "Anonim";
    private const string LeaderboardName = "Leaderboard";

    [SerializeField] private ViewLeaderboard _leaderboardView;

    private List<DataPlayer> _leaderboardPlayers = new List<DataPlayer>();
    private string AnonymousName;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case RussianCode:
                AnonymousName = AnonymousRu;
                break;
            case EnglishCode:
                AnonymousName = AnonymousEn;
                break;
            case TurkishCode:
                AnonymousName = AnonymousTr;
                break;
            default:
                AnonymousName = AnonymousEn;
                break;
        }
#endif
    }

    public void SetPlayer(int score)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result.score < score)
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, score);
            Debug.Log($"Result score = {result.score}/ Player Score = {score}");
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
            Debug.Log("Get Entries-----"+result);
            foreach (var entry in result.entries)
            {
                var name = entry.player.publicName;
                var rank = entry.rank;
                var score = entry.score;

                if (string.IsNullOrEmpty(name))
                {
                    name = AnonymousName;
                }
                Debug.Log($"{name}, {rank}, {score} -- Fill Player Data");
                _leaderboardPlayers.Add(new DataPlayer(name, rank, score));
            }
            Debug.Log(_leaderboardPlayers.Count+"___LeanerboardPlayerCount");
            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        });
    }
}
