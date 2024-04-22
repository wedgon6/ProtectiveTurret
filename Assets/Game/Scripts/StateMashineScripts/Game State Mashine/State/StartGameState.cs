﻿using ProtectiveTurret.GameControl;
using ProtectiveTurret.PlayerScripts;
using ProtectiveTurret.SaveAndLoad;
using ProtectiveTurret.ShoopAbillity;
using UnityEngine;

namespace ProtectiveTurret.StateMashineScripts
{
    public class StartGameState : GameState
    {
        [SerializeField] private Player _player;
        [SerializeField] private Shoop _shoop;
        [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;
        [SerializeField] private MenuTransition _menuTransition;
        [SerializeField] private TutorialTransition _tutorialTransition;
        [SerializeField] private AudioHandler _volumeChange;

        private bool _isFirstStart;

        public override void Enter()
        {
            base.Enter();
            _player.Initialize();
            _shoop.InitializeShop();
            _volumeChange.StartPlayMusic();

            if (_saveAndLoadSytem.TryGetSave())
            {
                _saveAndLoadSytem.GetCloudSaveData();
                _isFirstStart = false;
            }
            else
            {
                _isFirstStart = true;
            }

            if (_isFirstStart)
                _tutorialTransition.StartTutorial();
            else
                _menuTransition.ReturnToMeny();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}