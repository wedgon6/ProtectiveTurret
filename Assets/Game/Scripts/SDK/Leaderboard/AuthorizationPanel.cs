using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationPanel : MonoBehaviour
{
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private Button _closeButton;

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
        PlayerAccount.Authorize();
    }

    private void OnClose()
    {
        gameObject.SetActive(false);
    }
}
