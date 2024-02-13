using Agava.YandexGames;
using UnityEngine;

public class SaveAndLoadSytem : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Shoop _shoop;

    private GameInfo _gameInfo;
    private string _cloudSaveData;

    public string SaveData => _cloudSaveData;

#if UNITY_WEBGL && !UNITY_EDITOR
    public void SetCloudSaveData()
    {
        _gameInfo = new GameInfo(_player,_shoop.Abillities);
        _gameInfo.GetPlayerData();
       _cloudSaveData = JsonUtility.ToJson(_gameInfo);
        PlayerAccount.SetCloudSaveData(_cloudSaveData);
    }

    public void GetCloudSaveData()
    {
        PlayerAccount.GetCloudSaveData(OnSuccessCallback);
    }

    private void OnSuccessCallback(string gameInfo)
    {
        _gameInfo = JsonUtility.FromJson<GameInfo>(gameInfo);
        _player.SetPlayerData(_gameInfo.PlayerLvl, _gameInfo.PlayerMoney, _gameInfo.CurrentExperiancePlayer, _gameInfo.PlayerMoveSpeed, _gameInfo.PlayerScore);
        _shoop.SetAbillities(_gameInfo.PlayerAbillities);
    }
#endif
}
