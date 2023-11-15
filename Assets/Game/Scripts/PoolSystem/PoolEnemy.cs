using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolEnemy : MonoBehaviour, IPooling
{
    [SerializeField] private Transform _container;

    private List<Enemy> _enemiesInPool = new List<Enemy>();

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
