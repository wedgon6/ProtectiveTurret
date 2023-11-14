using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolEnemy : IPooling
{
    [SerializeField] private Transform _container;

    private const int CapasityEnemy = 3;
    private List<Enemy> _enemiesInPool = new List<Enemy>();

    public int Capasity => CapasityEnemy;

    public bool TryPoolObject(out IPoolObject result)
    {
        result = _enemiesInPool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void PoolObject(IPoolObject poolObject)
    {
        var enemy =  poolObject as Enemy;
        _enemiesInPool.Add(enemy);
    }
}
