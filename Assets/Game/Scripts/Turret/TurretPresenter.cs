using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : MonoBehaviour
{
    [SerializeField] private List<BaseTurret> _baseTurrets;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Player _player;

    public void TrySetTurret()
    {
        //if (_player.CurrentTurret != null)
        //    return;

        int playerLvl = _playerLevel.CurrentPlayerLvl;

        if (playerLvl >= _baseTurrets.Count)
        {
            _player.Initialize(_baseTurrets[_baseTurrets.Count]);
            return;
        }

        _player.Initialize(_baseTurrets[playerLvl - 1]);
    }
}
