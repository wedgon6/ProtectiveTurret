using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolBullet : MonoBehaviour, IPooling
{
    private List<Bullet> _bulletsInPool = new List<Bullet>(); 

    public void InstantiatePoolObject(IPoolObject poolObject)
    {
        poolObject.PoolReturned += PoolObject;
    }

    public void PoolObject(IPoolObject poolObject)
    {
        var bullet = poolObject as Bullet;
        _bulletsInPool.Add(bullet);
    }

    public bool TryPoolObject(out IPoolObject bullet)
    {
        bullet = _bulletsInPool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return bullet != null;
    }

    private void OnDisable()
    {
        foreach (var pollObject in _bulletsInPool)
        {
            pollObject.PoolReturned -= PoolObject;
        }
    }
}
