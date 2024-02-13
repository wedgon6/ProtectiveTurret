using UnityEngine;

namespace Assets.Game.Scripts.GameControl.StateMashine.State
{
    public class StartGameState : GameState
    {
        [SerializeField] private Player _player;
        [SerializeField] private Shoop _shoop;
        [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;
        [SerializeField] private MenuTransition _menuTransition;

        public override void Enter(Player player)
        {
            base.Enter(player);
            _player.Initialize();

            if (_saveAndLoadSytem.SaveData != string.Empty)
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                _saveAndLoadSytem.GetCloudSaveData();
#endif
            }

            _shoop.InitializeShop();
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
