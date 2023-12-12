using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : MonoBehaviour
{
    [SerializeField] private List<BaseTurret> _baseTurrets;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Player _player;

    private bool _isHaveTurret = false;

    public void TrySetTurret()
    {
        if (_isHaveTurret == true)
            return;

        int playerLvl = _playerLevel.CurrentPlayerLvl;

        if (playerLvl >= _baseTurrets.Count)
        {
            _player.Initialize(_baseTurrets[_baseTurrets.Count]);
            return;
        }

        _player.Initialize(_baseTurrets[playerLvl - 1]);
        _isHaveTurret = true;
    }
}
