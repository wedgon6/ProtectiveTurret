using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;

    private float _speedBullet;
    private Rigidbody _rigidbody;

    public void Initialize(float speedBullet)
    {
        _speedBullet = speedBullet;
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
