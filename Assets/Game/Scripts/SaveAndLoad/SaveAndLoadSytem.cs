using Agava.YandexGames;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class SaveAndLoadSytem : MonoBehaviour
{
    private const string DataKey = "PlayerDataTest1";

    [SerializeField] private Player _player;
    [SerializeField] private PlayerLevel _level;
    [SerializeField] private Shoop _shoop;
    [SerializeField] private TurretPresenter _turretPresenter;

    private GameInfo _gameInfo;
    private string _cloudSaveData;

    public string SaveData => _cloudSaveData;

    public bool TryGetSave()
    {
        if (Agava.YandexGames.Utility.PlayerPrefs.HasKey(DataKey))
        {
            _cloudSaveData = Agava.YandexGames.Utility.PlayerPrefs.GetString(DataKey);

            if (string.IsNullOrEmpty(_cloudSaveData))
            {
                Debug.Log("Строка пустая");
                return false;
            }


            Debug.Log("Строка НЕ пустая");
            return IsCorrectData();
        }
        else
        {
            return false;
        }
    }

    public void SetCloudSaveData()
    {
        _gameInfo = new GameInfo(_player, _shoop, _turretPresenter);
        _gameInfo.GetPlayerData();
       _cloudSaveData = JsonUtility.ToJson(_gameInfo);
        Debug.Log(_cloudSaveData);
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.Utility.PlayerPrefs.SetString(DataKey, _cloudSaveData);
        Agava.YandexGames.Utility.PlayerPrefs.Save();
#endif
    }

    public void GetCloudSaveData()
    {
        Debug.Log(_cloudSaveData+ "Гет сэйв");
        
        _player.SetPlayerData(_gameInfo.PlayerLvl, _gameInfo.PlayerMoney, _gameInfo.CurrentExperiancePlayer, _gameInfo.PlayerMoveSpeed, _gameInfo.PlayerScore);
        _shoop.SetAbillitiesData(_gameInfo.AbilitiesLvl, _gameInfo.AbilitiesPrise);
        _turretPresenter.SetCloudData(_gameInfo.IndexTurret,_gameInfo.AmmoSize,_gameInfo.ReloadTime);
    }

    private void OnEnable()
    {
        _level.OnDataChenged += OnGameDataChenged;
        _turretPresenter.OnDataChenged += OnGameDataChenged;
    }

    private void OnDisable()
    {
        _level.OnDataChenged -= OnGameDataChenged;
        _turretPresenter.OnDataChenged -= OnGameDataChenged;
    }

    private bool IsCorrectData()
    {
        _gameInfo = JsonUtility.FromJson<GameInfo>(_cloudSaveData);

        if(_gameInfo.PlayerLvl <= 0)
            return false;

        if (_gameInfo.AbilitiesLvl.Count == 0 || _gameInfo.AbilitiesPrise.Count == 0)
            return false;

        return true;
    }

    private void OnGameDataChenged()
    {
        Debug.Log("Вызов из события");
        SetCloudSaveData();
    }
}
