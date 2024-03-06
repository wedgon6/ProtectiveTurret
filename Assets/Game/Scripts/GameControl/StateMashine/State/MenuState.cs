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
    [SerializeField] private SizeAmmoView _sizeAmmo;
    [SerializeField] private MoneuView _moneuView;
    [SerializeField] private GameObject _settingsButton;

    private bool _isMenuUiActive = false;

    public override void Enter(Player player)
    {
        base.Enter(player);
        _menuUI.gameObject.SetActive(true);

        if (_isMenuUiActive == false)
            ActivatePlayerView();

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

    private void ActivatePlayerView()
    {
        _sizeAmmo.gameObject.SetActive(true);
        _moneuView.gameObject.SetActive(true);
        _settingsButton.SetActive(true);
        _isMenuUiActive = true;
    }

    private void Update()
    {
    }
}
