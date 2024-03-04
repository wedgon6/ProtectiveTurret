using Agava.YandexGames;
using UnityEngine;

namespace Assets.Game.Scripts.GameControl.StateMashine.State
{
    public class StartGameState : GameState
    {
        [SerializeField] private Player _player;
        [SerializeField] private Shoop _shoop;
        [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;
        [SerializeField] private MenuTransition _menuTransition;
        [SerializeField] private TutorialTransition _tutorialTransition;

        private bool _isFirstStart;

        public override void Enter(Player player)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif
            base.Enter(player);
            Debug.Log("EnterStaetState");
            _player.Initialize();
            _shoop.InitializeShop();

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

        private void Update()
        {
        }
    }
}
