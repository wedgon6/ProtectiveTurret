using Agava.YandexGames;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _setingsPanel;

    public void OpenLeaderboardView()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OpenLeaderboard();
#endif

        _leaderboardPanel.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.Fill();
#endif
        _setingsPanel.SetActive(false);
    }

    private void OpenLeaderboard()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == false)
            return;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();

    }
}
