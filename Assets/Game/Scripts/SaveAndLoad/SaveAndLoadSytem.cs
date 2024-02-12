using Agava.YandexGames;
using UnityEngine;

public class SaveAndLoadSytem : MonoBehaviour
{
    [SerializeField] private Player _player;

    private GameInfo _gameInfo;
    private string _cloudSaveData;
   
    public void SetCloudSaveData()
    {
        _gameInfo = new GameInfo(_player);
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
    }
}
