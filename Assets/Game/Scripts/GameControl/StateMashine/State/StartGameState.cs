using UnityEngine;

namespace Assets.Game.Scripts.GameControl.StateMashine.State
{
    public class StartGameState : GameState
    {
        [SerializeField] private Player _player;

        public override void Enter(Player player)
        {
            base.Enter(player);
        }
    }
}
