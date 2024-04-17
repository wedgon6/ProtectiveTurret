using UnityEngine;

public class WinState : GameState
{
    [SerializeField] private WinGamePanel _winGamePanel;
    [SerializeField] private AdvertisementPresenter _advertisementPresenter;

    public override void Enter()
    {
        _winGamePanel.gameObject.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        _advertisementPresenter.ShowInterstitialAd();
        base.Exit();
        _winGamePanel.gameObject.SetActive(false);
    }
}