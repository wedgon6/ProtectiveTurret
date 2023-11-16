using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolBullet : MonoBehaviour, IPooling
{
    private List<Bullet> _bulletsInPool = new List<Bullet>(); 

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
}
