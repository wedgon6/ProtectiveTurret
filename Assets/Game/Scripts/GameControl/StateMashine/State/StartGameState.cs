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
            base.Enter(player);
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif
            _player.Initialize();
            Debug.Log("Старт");
            Debug.Log("Проверка наличия сохранений");
            if (_saveAndLoadSytem.TryGetSave())
            {
                Debug.Log("Сохранения есть");

                _saveAndLoadSytem.GetCloudSaveData();
                _isFirstStart = false;

            }
            else
            {
                _isFirstStart = true;
            }

            _shoop.InitializeShop();

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
