using Agava.YandexGames;
using System;
using UnityEngine;

public class SaveAndLoadSytem : MonoBehaviour
{
    private const string DataKey = "PlayerData";

    [SerializeField] private Player _player;
    [SerializeField] private Shoop _shoop;
    [SerializeField] private TurretPresenter _turretPresenter;

    private GameInfo _gameInfo;
    private string _cloudSaveData;

    public string SaveData => _cloudSaveData;

    public bool TryGetSave()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("Save check");
        Debug.Log(_cloudSaveData);
#endif
        if (Agava.YandexGames.Utility.PlayerPrefs.HasKey(DataKey))
        {
            _cloudSaveData = Agava.YandexGames.Utility.PlayerPrefs.GetString(DataKey);
            return true;
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
       _cloudSaveData = JsonUtility.ToJson(_gameInfo+"Set save");
        Debug.Log(_cloudSaveData);
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.Utility.PlayerPrefs.SetString(DataKey, _cloudSaveData);
        Agava.YandexGames.Utility.PlayerPrefs.Save();
       
        Debug.Log("Проверка ключа ---" + Agava.YandexGames.Utility.PlayerPrefs.HasKey(DataKey));
        Debug.Log(_cloudSaveData);
#endif
    }

    public void GetCloudSaveData()
    {
        Debug.Log(_cloudSaveData+"Get save");
        _gameInfo = JsonUtility.FromJson<GameInfo>(_cloudSaveData);

        _cloudSaveData = string.Empty;
        Agava.YandexGames.Utility.PlayerPrefs.SetString(DataKey, _cloudSaveData);
        Agava.YandexGames.Utility.PlayerPrefs.Save();
        
        _player.SetPlayerData(_gameInfo.PlayerLvl, _gameInfo.PlayerMoney, _gameInfo.CurrentExperiancePlayer, _gameInfo.PlayerMoveSpeed, _gameInfo.PlayerScore);
        _shoop.SetAbillitiesData(_gameInfo.AbilitiesLvl, _gameInfo.AbilitiesPrise);
        _turretPresenter.SetCloudData(_gameInfo.IndexTurret,_gameInfo.AmmoSize,_gameInfo.ReloadTime);
#if UNITY_WEBGL && !UNITY_EDITOR
#endif
    }

    private void OnEnable()
    {
        _player.OnDataChenged += OnGameDataChenged;
        _turretPresenter.OnDataChenged += OnGameDataChenged;
    }

    private void OnDisable()
    {
        _player.OnDataChenged -= OnGameDataChenged;
        _turretPresenter.OnDataChenged -= OnGameDataChenged;
    }

    private void OnGameDataChenged()
    {
        SetCloudSaveData();
    }
}
