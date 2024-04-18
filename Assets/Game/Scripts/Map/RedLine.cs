using ProtectiveTurret.EnemyScripts;
using System;
using UnityEngine;

namespace ProtectiveTurret.Map
{
    public class RedLine : MonoBehaviour
    {
        public event Action GameLoosed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                GameLoosed?.Invoke();
        }
    }
}