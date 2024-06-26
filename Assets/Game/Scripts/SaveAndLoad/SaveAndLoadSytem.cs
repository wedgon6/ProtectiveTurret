using Agava.YandexGames;
using ProtectiveTurret.PlayerScripts;
using ProtectiveTurret.ShoopAbillity;
using ProtectiveTurret.TurretScripts;
using UnityEngine;

namespace ProtectiveTurret.SaveAndLoad
{
    public class SaveAndLoadSytem : MonoBehaviour
    {
        private const string DataKeyCloud = "PlayerDataTest1";
        private const string DataKeyLocal = "PlayerDataLocal";

        [SerializeField] private Player _player;
        [SerializeField] private PlayerLevel _level;
        [SerializeField] private Shoop _shoop;
        [SerializeField] private TurretPresenter _turretPresenter;

        private GameInfo _gameInfo;
        private string _saveData;

        public string SaveData => _saveData;

        public bool TryGetSave()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            if (Agava.YandexGames.Utility.PlayerPrefs.HasKey(DataKeyCloud))
                _saveData = Agava.YandexGames.Utility.PlayerPrefs.GetString(DataKeyCloud);
            else
                return false;
        }

        if(PlayerAccount.IsAuthorized == false)
        {
            if (PlayerPrefs.HasKey(DataKeyLocal))
                _saveData = PlayerPrefs.GetString(DataKeyLocal);
            else
                return false;
        }

        return IsCorrectData();
#endif
            return false;
        }

        public void SetSaveData()
        {
            _gameInfo = new GameInfo(_player, _shoop, _turretPresenter);
            _gameInfo.GetPlayerData();
            _saveData = JsonUtility.ToJson(_gameInfo);

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            Agava.YandexGames.Utility.PlayerPrefs.SetString(DataKeyCloud, _saveData);
            Agava.YandexGames.Utility.PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetString(DataKeyLocal, _saveData);
            PlayerPrefs.Save();
        }
#endif
        }

        public void GetCloudSaveData()
        {
            _player.SetPlayerData(_gameInfo.PlayerLvl, _gameInfo.PlayerMoney, _gameInfo.CurrentExperiancePlayer, _gameInfo.PlayerMoveSpeed, _gameInfo.PlayerScore);
            _shoop.SetAbillitiesData(_gameInfo.AbilitiesLvl, _gameInfo.AbilitiesPrise);
            _turretPresenter.SetCloudData(_gameInfo.IndexTurret, _gameInfo.AmmoSize, _gameInfo.ReloadTime);
        }

        private void OnEnable()
        {
            _level.DataChanged += OnGameDataChenged;
            _turretPresenter.DataChanged += OnGameDataChenged;
        }

        private void OnDisable()
        {
            _level.DataChanged -= OnGameDataChenged;
            _turretPresenter.DataChanged -= OnGameDataChenged;
        }

        private bool IsCorrectData()
        {
            if (string.IsNullOrEmpty(_saveData))
                return false;

            _gameInfo = JsonUtility.FromJson<GameInfo>(_saveData);

            if (_gameInfo.PlayerLvl <= 0)
                return false;

            if (_gameInfo.AbilitiesLvl.Count == 0 || _gameInfo.AbilitiesPrise.Count == 0)
                return false;

            return true;
        }

        private void OnGameDataChenged() => SetSaveData();
    }
}