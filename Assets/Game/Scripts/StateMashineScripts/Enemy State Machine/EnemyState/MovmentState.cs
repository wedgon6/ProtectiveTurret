using UnityEngine;
using UnityEngine.AI;

namespace ProtectiveTurret.StateMashineScripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovmentState : EnemyState
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void FixedUpdate()
        {
            _agent.SetDestination(Vector3.forward + Target.transform.position);
        }
    }
}