using UnityEngine;

public class BattleState : GameState
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private WaveProgressBar _progressBar;
    [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;
    [SerializeField] private AdvertisementPresenter _advertisementPresenter;

    public override void Enter(Player player)
    {
        _progressBar.gameObject.SetActive(true);
        base.Enter(player);
        _player.ResetTurret();
        _player.SetMovmentMode(true);
        _spawner.RestSpawner();
    }

    public override void Exit()
    {
        _advertisementPresenter.ShowInterstitialAd();

        _saveAndLoadSytem.SetCloudSaveData();

        base.Exit();
        _progressBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
