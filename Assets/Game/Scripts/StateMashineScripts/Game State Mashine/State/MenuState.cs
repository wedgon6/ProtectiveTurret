using ProtectiveTurret.PlayerScripts;
using ProtectiveTurret.SaveAndLoad;
using ProtectiveTurret.SDK;
using ProtectiveTurret.TurretScripts;
using ProtectiveTurret.UI;
using UnityEngine;

namespace ProtectiveTurret.StateMashineScripts
{
    public class MenuState : GameState
    {
        private const float _startPositionX = -1.55f;
        private const float _startPositionY = -5.32f;
        private const float _startPositionZ = 4.96f;

        [SerializeField] private Player _player;
        [SerializeField] private MovementPlayer _movementPlayer;
        [SerializeField] private MenuPanel _menuUI;
        [SerializeField] private TurretPresenter _turretPresenter;
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;
        [SerializeField] private UserView _userView;

        private bool _isMenuUiActive = false;

        public override void Enter()
        {
            base.Enter();
            _menuUI.gameObject.SetActive(true);

            if (_isMenuUiActive == false)
                ActivatePlayerView();

            _turretPresenter.TrySetTurret();
            _movementPlayer.SetModeMovmen(false);
            _player.transform.position = new Vector3(_startPositionX, _startPositionY, _startPositionZ);
#if UNITY_WEBGL && !UNITY_EDITOR
        _leaderboard.SetPlayer(_player.CurrentScore);
#endif
        }

        public override void Exit()
        {
            _menuUI.gameObject.SetActive(false);
#if UNITY_WEBGL && !UNITY_EDITOR
        _saveAndLoadSytem.SetSaveData();
#endif
            _movementPlayer.SetModeMovmen(true);
            base.Exit();
        }

        private void ActivatePlayerView()
        {
            _userView.gameObject.SetActive(true);
            _isMenuUiActive = true;
        }
    }
}