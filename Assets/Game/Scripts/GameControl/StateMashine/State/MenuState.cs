using UnityEngine;

public class MenuState : GameState
{
    private const float _startPositionX = -1.55f;
    private const float _startPositionY = -5.32f;
    private const float _startPositionZ = 4.96f;

    [SerializeField] private Player _player;
    [SerializeField] private MenuPanel _menuUI;
    [SerializeField] private TurretPresenter _turretPresenter;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private AdvertisementPresenter _advertisementPresenter;
    [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;

    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Μενώ");
        Debug.Log(string.IsNullOrEmpty(_saveAndLoadSytem.SaveData));
        _menuUI.gameObject.SetActive(true);
        _turretPresenter.TrySetTurret();
        _player.SetMovmentMode(false);
        _player.transform.position = new Vector3(_startPositionX, _startPositionY, _startPositionZ);
        _player.RotationTurret(150);
        _leaderboard.SetPlayer(_player.CurrentScore);
    }

    public override void Exit()
    {
        _advertisementPresenter.ShowInterstitialAd();
        _menuUI.gameObject.SetActive(false);
        _player.RotationTurret(0);


        _saveAndLoadSytem.SetCloudSaveData();


        base.Exit();
    }

    private void Update()
    {
    }
}
