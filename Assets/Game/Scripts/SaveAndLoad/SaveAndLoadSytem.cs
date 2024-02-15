using Agava.YandexGames;
using UnityEngine;

public class SaveAndLoadSytem : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Shoop _shoop;

    private GameInfo _gameInfo;
    private string _cloudSaveData;

    public string SaveData => _cloudSaveData;


    public void SetCloudSaveData()
    {
        _gameInfo = new GameInfo(_player,_shoop.Abillities);
        _gameInfo.GetPlayerData();
       _cloudSaveData = JsonUtility.ToJson(_gameInfo);
        Debug.Log("Засэйвил");
        Debug.Log(string.IsNullOrEmpty(_cloudSaveData));
        Debug.Log(_cloudSaveData);
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.SetCloudSaveData(_cloudSaveData);
#endif
    }

    public void GetCloudSaveData()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.GetCloudSaveData(OnSuccessCallback);
#endif
    }

    private void OnSuccessCallback(string json)
    {
        Debug.Log("Вызва колбэк");
        _gameInfo = JsonUtility.FromJson<GameInfo>(_cloudSaveData);
        _player.SetPlayerData(_gameInfo.PlayerLvl, _gameInfo.PlayerMoney, _gameInfo.CurrentExperiancePlayer, _gameInfo.PlayerMoveSpeed, _gameInfo.PlayerScore);
        _shoop.SetAbillities(_gameInfo.PlayerAbillities);
    }
}
