using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationPanel : MonoBehaviour
{
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Leaderboard _leaderboard;

    private void OnEnable()
    {
        _authorizationButton.onClick.AddListener(OnAuthorization);
        _closeButton.onClick.AddListener(OnClose);
    }

    private void OnDisable()
    {
        _authorizationButton.onClick.RemoveListener(OnAuthorization);
        _closeButton.onClick.RemoveListener(OnClose);
    }

    private void OnAuthorization()
    {
        PlayerAccount.Authorize(onRequestDataPermission);
    }

    private void OnClose()
    {
        gameObject.SetActive(false);
    }

    private void onRequestDataPermission()
    {
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();

        _leaderboard.Fill();
        gameObject.SetActive(false);
    }
}
