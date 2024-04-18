using ProtectiveTurret.EnemyScripts;
using ProtectiveTurret.Map;
using ProtectiveTurret.PoolSystem;
using UnityEngine;

namespace ProtectiveTurret.TurretScripts
{
    public class Bullet : PoolObject
    {
        [SerializeField] private float _damage;

        private Rigidbody _rigidbody;

        protected override void ReturnToPool()
        {
            base.ReturnToPool();
            _rigidbody.velocity = Vector3.zero;
            transform.position = Vector3.zero;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
                ReturnToPool();
            }
            else if (collision.collider.TryGetComponent(out Barrier barrier) || collision.collider.TryGetComponent(out Terrain terrain))
            {
                ReturnToPool();
            }
        }
    }
}