using Agava.YandexGames;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private GameObject _leaderboardPanel;

    public void OpenLeaderboardView()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OpenLeaderboard();
#endif
        _leaderboardPanel.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.Fill();
#endif
    }

    private void OpenLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();
        
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
    }
}
