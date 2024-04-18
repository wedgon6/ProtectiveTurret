﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtectiveTurret.PoolSystem
{
    public class Pool : MonoBehaviour
    {
        private List<PoolObject> _poolObjects = new List<PoolObject>();

        public void InstantiatePoolObject(PoolObject poolObject)
        {
            poolObject.PoolReturned += PoolObject;
        }

        public void PoolObject(PoolObject poolObject)
        {
            _poolObjects.Add(poolObject);
        }

        public bool TryPoolObject(out PoolObject bullet)
        {
            bullet = _poolObjects.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return bullet != null;
        }

        private void OnDisable()
        {
            foreach (var pollObject in _poolObjects)
            {
                pollObject.PoolReturned -= PoolObject;
            }
        }
    }
}