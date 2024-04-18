using UnityEngine;

namespace ProtectiveTurret.StateMashineScripts
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public State TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}