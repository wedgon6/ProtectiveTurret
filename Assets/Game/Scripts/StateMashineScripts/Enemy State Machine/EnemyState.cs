using ProtectiveTurret.EnemyScripts;
using ProtectiveTurret.Map;
using ProtectiveTurret.PlayerScripts;
using UnityEngine;

namespace ProtectiveTurret.StateMashineScripts
{
    [RequireComponent(typeof(Enemy))]
    public abstract class EnemyState : State
    {
        private Enemy _enemy;

        public Enemy Enemy => _enemy;

        protected RedLine Target { get; set; }
        protected PlayerScore PlayerScore { get; set; }
        protected PlayerMoney PlayerMoney { get; set; }

        protected override void OnEnter()
        {
            _enemy = GetComponent<Enemy>();
            Target = _enemy.Target;
            PlayerScore = _enemy.PlayerScore;
            PlayerMoney = _enemy.PlayerMoney;
        }
    }
}