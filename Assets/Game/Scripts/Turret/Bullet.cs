using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _damage;

    private float _speedBullet;
    private Rigidbody _rigidbody;
    private PoolBullet _poolBullet;
    protected Enemy _enemy;

    public void Initialize(float speedBullet, PoolBullet poolBullet, Enemy target)
    {
        _speedBullet = speedBullet;
        _poolBullet = poolBullet;
        _enemy = target; 
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

        if (collision.collider.TryGetComponent(out Terrain terrain))
        {
            ReturnToPool();
        }
    }
}
