using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawnerEnemy : Spawner
{
    [SerializeField] private GameObject _target;

    public override SpawnableObject Pull(Vector3 spawnPoint)
    {
        if (_spawnableObjectsQueue.Count == 0)
        {
            Instantiate(_spawnableObject, spawnPoint, Quaternion.identity).Initialize(_target,this).Push();
        }

        var spawned = _spawnableObjectsQueue.Dequeue();
        spawned.Pull(spawnPoint);

        return spawned;
    }
}
