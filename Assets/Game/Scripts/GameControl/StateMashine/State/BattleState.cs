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
        Debug.Log("Сохранение после завершения боя");
        _saveAndLoadSytem.SetCloudSaveData();
        _progressBar.gameObject.SetActive(false);
        _advertisementPresenter.ShowInterstitialAd();
        base.Exit();
    }

    private void Update()
    {
        
    }
}
