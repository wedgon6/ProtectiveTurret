using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : MonoBehaviour
{
    [SerializeField] private List<Turret> _baseTurrets;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Player _player;

    private bool _isHaveTurret = false;
    private int _indexTurret = 0;
    private Turret _currentTurret;

    private float _baseReloadTime = 4f;
    private float _decreaseReload = 0.16f;
    private int _baseAmmouSize = 20;
    private int _ammouSizeRise = 15;

    public event Action DataChanged;

    public int CurrentIndexTurret => _indexTurret;
    public int CurrentAmmoSize => _baseAmmouSize;
    public float CurrentReloadTime => _baseReloadTime;

    public void SetCloudData(int index, int ammoSize, float reloadTime)
    {
        _indexTurret = index;
        _baseAmmouSize = ammoSize;
        _baseReloadTime = reloadTime;
    }

    public void AddAmmouTurret()
    {
        _baseAmmouSize += _ammouSizeRise;
        _player.CurrentTurret.SetTurretParameters(_baseAmmouSize, _baseReloadTime);
    }

    public void ReduceCooldownReload()
    {
        if (_baseReloadTime <= _decreaseReload)
            return;

        _baseReloadTime -= _decreaseReload;
        _player.CurrentTurret.SetTurretParameters(_baseAmmouSize, _baseReloadTime);
    }

    public void TrySetTurret()
    {
        if (_isHaveTurret == true)
            return;

        InitializePlayerTurret();
    }

    private void OnEnable()
    {
        _playerLevel.PlayerLvlChanged += OnPlayerLvlUp;
    }

    private void OnDisable()
    {
        _playerLevel.PlayerLvlChanged -= OnPlayerLvlUp;
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
                DataChanged?.Invoke();
            }
        }
    }

    private void InitializePlayerTurret()
    {
        if(_playerLevel.CurrentPlayerLvl == 1)
        {
            _currentTurret = _baseTurrets[0];
            _isHaveTurret = true;
            _player.InitializeTurret(_currentTurret, _baseAmmouSize, _baseReloadTime);
        }
        else if (_indexTurret >= _baseTurrets.Count)
        {
            _currentTurret = _baseTurrets[_baseTurrets.Count-1];
            _player.InitializeTurret(_currentTurret, _baseAmmouSize, _baseReloadTime);
        }
        else
        {
            _currentTurret = _baseTurrets[_indexTurret];
            _player.InitializeTurret(_currentTurret, _baseAmmouSize, _baseReloadTime);
        }
    }
}
