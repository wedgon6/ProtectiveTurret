using System;
using UnityEngine;

namespace ProtectiveTurret.PoolSystem
{
    public class PoolObject : MonoBehaviour
    {
        public event Action<PoolObject> PoolReturned;

        public void ReturObjectPool()
        {
            ReturnToPool();
        }

        protected virtual void ReturnToPool()
        {
            gameObject.SetActive(false);
            PoolReturned?.Invoke(this);
        }
    }
}