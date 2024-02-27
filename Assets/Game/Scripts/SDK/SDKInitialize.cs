using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitialize : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
   private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        if (PlayerAccount.IsAuthorized)
        {
            Agava.YandexGames.Utility.PlayerPrefs.Load(OnSuccessColback, OnErrorColbak);
        }
        else
        {
            SceneManager.LoadScene("BaseScene");
        }
    }

    private void OnSuccessColback()
    {
        SceneManager.LoadScene("BaseScene");
    }

    private void OnErrorColbak(string error)
    {
        Debug.Log("ErrorColback");
        SceneManager.LoadScene("BaseScene");
    }
#else
    private void Start()
    {
        SceneManager.LoadScene("BaseScene");
    }
#endif
}
