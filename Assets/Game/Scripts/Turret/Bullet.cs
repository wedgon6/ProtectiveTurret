using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _damage;

    private Rigidbody _rigidbody;

    public event Action<IPoolObject> PoolReturned;

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        PoolReturned?.Invoke(this);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            ReturnToPool();
        }
        else if(collision.collider.TryGetComponent(out Barrier barrier) || collision.collider.TryGetComponent(out Terrain terrain))
        {
            ReturnToPool();
        }
    }
}
