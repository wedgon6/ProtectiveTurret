using Agava.YandexGames;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private GameObject _leaderboardPanel;

    public void OpenLeaderboardView()
    {
        _leaderboardPanel.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.Fill();
#endif

    }

    private void OpenLeaderboard()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == false)
            return;

        if(PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();

    }
}
