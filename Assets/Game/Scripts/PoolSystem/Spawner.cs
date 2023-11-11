using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected SpawnableObject _spawnableObject;

    protected Queue<SpawnableObject> _spawnableObjectsQueue = new Queue<SpawnableObject>();

    public abstract SpawnableObject Pull(Vector3 spawnPoint);
    //{
    //    if (_spawnableObjectsQueue.Count == 0)
    //    {
    //        Instantiate(_spawnableObject, spawnPoint, Quaternion.identity).Initialize(this).Push();
    //    }

    //    var spawned = _spawnableObjectsQueue.Dequeue();
    //    spawned.Pull(spawnPoint);

    //    return spawned;
    //}

    internal void Push(SpawnableObject spawnableObject)
    {
        spawnableObject.SetActive(false);
        _spawnableObjectsQueue.Enqueue(spawnableObject);
    }
}
