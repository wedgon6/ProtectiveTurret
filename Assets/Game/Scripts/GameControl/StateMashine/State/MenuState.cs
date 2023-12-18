using UnityEngine;

public class MenuState : GameState
{
    [SerializeField] private Player _player;
    [SerializeField] private MenuPanel _menuUI;
    [SerializeField] private TurretPresenter _turretPresenter;

    private const float _startPositionX = -1.55f;
    private const float _startPositionY = -5.32f;
    private const float _startPositionZ = 4.96f;

    private void Awake()
    {
        _player.Initialize();
    }

    public override void Enter(Player player)
    {
        base.Enter(player);
        _menuUI.gameObject.SetActive(true);
        _turretPresenter.TrySetTurret();
        _player.transform.position = new Vector3(_startPositionX, _startPositionY, _startPositionZ);
        _player.RotationTurret(150);
    }

    public override void Exit()
    {
        _menuUI.gameObject.SetActive(false);
        _player.RotationTurret(0);
        base.Exit();
    }

    private void Update()
    {
        
    }
}
