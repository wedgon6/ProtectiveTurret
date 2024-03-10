using UnityEngine;
using Agava.YandexGames;

public class AdvertisementPresenter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private VolumeChange _volumeChange;
    [SerializeField] private int _revard = 500;

    public void ShowRewardAd() =>
        VideoAd.Show(OnOpenCallBack, OnRewardedCallback, OnCloseCallBack);

    public void ShowInterstitialAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
#endif
    }

    private void OnOpenCallBack()
    {
        AudioListener.volume = 0f;
        Time.timeScale = 0f;
    }

    private void OnCloseCallBack(bool canShow)
    {
        if (!canShow)
            return;

        OnCloseCallBack();
    }

    private void OnCloseCallBack()
    {
        if (_volumeChange.IsAudioPlay)
            AudioListener.volume = 1f;

        Time.timeScale = 1f;
    }

    private void OnRewardedCallback()
    {
        _player.AddMoney(_revard);
    }
}
