using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    private GameObject _gameObject;
    private Transform _transform;
    private Spawner _spawner;
    private Rigidbody _rigidbody;

    private float _pushTimer = -1;

    internal virtual SpawnableObject Initialize<T>(T param, Spawner spawner)
    {
        _spawner = spawner;
        return this;
    }

    public abstract void Push();
    //{
    //    _spawner.Push(this);
    //}

    public abstract SpawnableObject Pull(Vector3 position);
    //{
    //    _transform.position = position;
    //    SetActive(true);
    //    return this;
    //}

    public void SetActive(bool isActive)
    {
        _gameObject.SetActive(isActive);
    }

    internal void SetVelosity(Vector3 vector3)
    {
        _rigidbody.velocity = vector3;
    }

    internal void PushDelayed(float time)
    {
        _pushTimer = time;
    }
}
