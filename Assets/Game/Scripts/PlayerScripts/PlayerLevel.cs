using System;
using UnityEngine;

namespace ProtectiveTurret.PlayerScripts
{
    public class PlayerLevel : MonoBehaviour
    {
        private const int ExperienceToNextLvlup = 3;
        private const int StartPalerLvl = 1;

        private int _currentPlayerLvl = StartPalerLvl;
        private int _currentExperience = 0;

        public event Action<int, int> PlayerExpirianceChanged;
        public event Action PlayerLvlChanged;
        public event Action DataChanged;
        public event Action LvLPlayerSet;

        public int ExperienceToNextLvl => ExperienceToNextLvlup;
        public int CurrentExperience => _currentExperience;
        public int CurrentPlayerLvl => _currentPlayerLvl;

        public void SetData(int currentLvl, int currentExperiance)
        {
            _currentPlayerLvl = currentLvl;
            _currentExperience = currentExperiance;
            PlayerExpirianceChanged?.Invoke(_currentExperience, ExperienceToNextLvlup);
            LvLPlayerSet?.Invoke();
        }

        public void AddExperience()
        {
            _currentExperience++;
            PlayerExpirianceChanged?.Invoke(_currentExperience, ExperienceToNextLvlup);

            if (_currentExperience == ExperienceToNextLvlup)
            {
                _currentExperience = 0;
                _currentPlayerLvl++;
                PlayerExpirianceChanged?.Invoke(_currentExperience, ExperienceToNextLvlup);
                PlayerLvlChanged?.Invoke();
            }

            DataChanged?.Invoke();
        }
    }
}