using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState
{
    [SerializeField] private Player _player;
    [SerializeField] private MenuPanel _menuUI;
    [SerializeField] private TurretPresenter _turretPresenter;

    public override void Enter(Player player)
    {
        base.Enter(player);
        _menuUI.gameObject.SetActive(true);
        _turretPresenter.TrySetTurret();
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
