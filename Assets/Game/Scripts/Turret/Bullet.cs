using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _damage;

    private float _speedBullet;
    private Rigidbody _rigidbody;
    private PoolBullet _poolBullet;

    public void Initialize(float speedBullet, PoolBullet poolBullet)
    {
        _speedBullet = speedBullet;
        _poolBullet = poolBullet;
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _poolBullet.PoolObject(this);
        _rigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward * _speedBullet, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            ReturnToPool();
        }

        if(collision.collider.TryGetComponent(out Barrier barrier))
        {
            ReturnToPool();
        }
    }
}
