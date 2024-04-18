using ProtectiveTurret.EnemyScripts;
using UnityEngine;

namespace ProtectiveTurret.StateMashineScripts
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyStateMachine : StateMashine
    {
        public void ResetStete()
        {
            EnterState(_firstState);
        }

        private void Start()
        {
            EnterState(_firstState);
        }

        private void Update()
        {
            UpdateStateStatus();
        }
    }
}