using Agava.YandexGames;
using UnityEngine;

public class SaveAndLoadSytem : MonoBehaviour
{
    private const string DataKey = "PlayerData";

    [SerializeField] private Player _player;
    [SerializeField] private Shoop _shoop;

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
            Debug.Log("True");
            return true;
        }
        else
        {
            Debug.Log("False");
            return false;
        }
    }

    public void SetCloudSaveData()
    {
        _gameInfo = new GameInfo(_player, _shoop);
        _gameInfo.GetPlayerData();
       _cloudSaveData = JsonUtility.ToJson(_gameInfo);
        Debug.Log("Засэйвил");
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.Utility.PlayerPrefs.SetString(DataKey, _cloudSaveData);
        Agava.YandexGames.Utility.PlayerPrefs.Save();
       
        Debug.Log("Проверка ключа ---" + Agava.YandexGames.Utility.PlayerPrefs.HasKey(DataKey));
        Debug.Log(_cloudSaveData);
#endif
    }

    public void GetCloudSaveData()
    {
        Debug.Log("Вызов GetSave");
#if UNITY_WEBGL && !UNITY_EDITOR
        _gameInfo = JsonUtility.FromJson<GameInfo>(_cloudSaveData);
        _player.SetPlayerData(_gameInfo.PlayerLvl, _gameInfo.PlayerMoney, _gameInfo.CurrentExperiancePlayer, _gameInfo.PlayerMoveSpeed, _gameInfo.PlayerScore);
        _shoop.SetAbillitiesData(_gameInfo.AbilitiesLvl, _gameInfo.AbilitiesPrise);
#endif
    }
}
