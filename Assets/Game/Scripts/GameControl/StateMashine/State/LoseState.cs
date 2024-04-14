using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseGamePanel _loseGamePanel;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private AdvertisementPresenter _advertisementPresenter;

    public override void Enter(Player player)
    {
        _loseGamePanel.gameObject.SetActive(true);
        _player.LoseGame(false);
        _spawner.PutEnemyToPool();
        base.Enter(player);
    }

    public override void Exit()
    {
        _advertisementPresenter.ShowInterstitialAd();
        base.Exit();
        _loseGamePanel.gameObject.SetActive(false);
        _player.LoseGame(true);
    }
}