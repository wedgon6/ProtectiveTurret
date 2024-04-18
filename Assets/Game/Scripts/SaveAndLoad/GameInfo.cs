using ProtectiveTurret.PlayerScripts;
using ProtectiveTurret.ShoopAbillity;
using ProtectiveTurret.TurretScripts;
using System;
using System.Collections.Generic;

namespace ProtectiveTurret.SaveAndLoad
{
    [Serializable]
    public class GameInfo
    {
        public int PlayerLvl;
        public int PlayerMoney;
        public int CurrentExperiancePlayer;
        public float PlayerMoveSpeed;
        public int PlayerScore;

        public List<int> AbilitiesLvl = new List<int>();
        public List<int> AbilitiesPrise = new List<int>();

        public int IndexTurret;
        public int AmmoSize;
        public float ReloadTime;

        private Player _player;
        private Shoop _shoop;
        private TurretPresenter _turretPresenter;

        public GameInfo(Player player, Shoop shoop, TurretPresenter turretPresenter)
        {
            _player = player;
            _shoop = shoop;
            _turretPresenter = turretPresenter;
        }

        public void GetPlayerData()
        {
            PlayerLvl = _player.CurrentLvl;
            PlayerMoney = _player.CurrentMoney;
            CurrentExperiancePlayer = _player.CurrenExpereance;
            PlayerMoveSpeed = _player.CurrentMoveSpeed;
            PlayerScore = _player.CurrentScore;

            for (int i = 0; i < _shoop.Abillities.Count; i++)
            {
                AbilitiesLvl.Add(_shoop.Abillities[i].CurrentLvlAbillity);
            }

            for (int i = 0; i < _shoop.Abillities.Count; i++)
            {
                AbilitiesPrise.Add(_shoop.Abillities[i].Price);
            }

            IndexTurret = _turretPresenter.CurrentIndexTurret;
            AmmoSize = _turretPresenter.CurrentAmmoSize;
            ReloadTime = _turretPresenter.CurrentReloadTime;
        }
    }
}