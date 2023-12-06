using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState
{
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Player _player;
    [SerializeField] private List<BaseTurret> _turretPrefab;
    [SerializeField] private MenuPanel _menuUI;

    public override void Enter(Player player)
    {
        _menuUI.gameObject.SetActive(true);
        _player.RotationTurret(150);
        base.Enter(player);
    }

    public override void Exit()
    {
        _menuUI.gameObject.SetActive(false);
        _player.RotationTurret(0);
        base.Exit();
    }

    private void Awake()
    {
        SetTurret(_playerLevel.CurrentPlayerLvl);
    }

    private void SetTurret(int playerLvl)
    {
        if (playerLvl >= _turretPrefab.Count)
        {
            _player.Initialize(_turretPrefab[_turretPrefab.Count]);
            return;
        }

        _player.Initialize(_turretPrefab[playerLvl - 1]);
    }
}
