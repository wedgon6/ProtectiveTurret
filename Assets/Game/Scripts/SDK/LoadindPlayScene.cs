using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadindPlayScene : MonoBehaviour
{
    private const string SceneName = "BaseScene";

    [SerializeField] private Image _loadingImage;

    private AsyncOperation _asyncOperation;
    private Coroutine _coroutine;

    public void StartLoadScene()
    {
        _asyncOperation.allowSceneActivation = true;
    }

    private void Start()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        _asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        _asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
            float progress = _asyncOperation.progress / 0.9f;
            _loadingImage.fillAmount = progress;
            yield return null;
        }
    }
}