using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : MonoBehaviour
{
    [SerializeField] private List<BaseTurret> _baseTurrets;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Player _player;
    [SerializeField] private SizeClipUI _sizeClipTurret;

    private bool _isHaveTurret = false;
    private int _indexTurret = 0;

    public void TrySetTurret()
    {
        if (_isHaveTurret == true)
            return;

       InitializePlayerTurret();
       _sizeClipTurret.SetTurret(_baseTurrets[_indexTurret]);
    }

    private void OnEnable()
    {
        _playerLevel.OnLvlUp += OnPlayerLvlUp;
    }

    private void OnDisable()
    {
        _playerLevel.OnLvlUp -= OnPlayerLvlUp;
    }

    private void OnPlayerLvlUp()
    {
        if(_playerLevel.CurrentPlayerLvl % 2 == 0)
        {
            if (_indexTurret > _baseTurrets.Count)
            {
                return;
            }
            else
            {
                _indexTurret++;
                InitializePlayerTurret();
            }
        }
    }

    private void InitializePlayerTurret()
    {
        if(_playerLevel.CurrentPlayerLvl == 1)
        {
            _player.InitializeTurret(_baseTurrets[0]);
            _isHaveTurret = true;
            return;
        }

        if (_indexTurret >= _baseTurrets.Count)
        {
            Debug.Log($"{_baseTurrets.Count}");
            _player.InitializeTurret(_baseTurrets[_baseTurrets.Count]);
            return;
        }

        _player.InitializeTurret(_baseTurrets[_indexTurret]);
        _sizeClipTurret.SetTurret(_baseTurrets[_indexTurret]);
    }
}
