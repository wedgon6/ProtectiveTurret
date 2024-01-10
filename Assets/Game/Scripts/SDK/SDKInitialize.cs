using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitialize : MonoBehaviour
{
#if !UNITY_EDITOR
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
        SceneManager.LoadScene("BaseScene");
    }
#else
    private void Awake()
    {
        SceneManager.LoadScene("BaseScene");
    }
#endif
}
