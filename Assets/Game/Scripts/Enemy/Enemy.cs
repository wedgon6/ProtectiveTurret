using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    private GameObject _target;

    public float Health => _health;

    public GameObject Target => _target;

    public void Initialize(GameObject target)
    {
        _target = target;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        if (_health < 0)
            return;

        _health -= damage;

        if (_health < 0)
        {
            _health = 0;
        }
    }
}
