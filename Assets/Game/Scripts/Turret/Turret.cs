using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Turret : MonoBehaviour
{
    protected const float SearchRadius = 20f;
    protected const float SpeedBullet = 15f;

    [SerializeField] private ShootPoint[] _shootPoints;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private PoolBullet _poolBullet;
    [SerializeField] private float _delayShot = 0.25f;

    private Dictionary<float, Enemy> _enemies = new Dictionary<float, Enemy>();

    private Enemy _currentTarget;
    private Coroutine _corontine;
    private Animator _animator;

    private int _ammouSize;
    private int _currentCoutBullet;
    private float _cooldownReload;

    public event Action ClipSizeChanged;
    public event Action Shoted;
    public event Action AmmouRecharging;

    public int CurrentCountBullet => _currentCoutBullet;
    public float CoolDonw => _cooldownReload;

    public void SetTurretParameters(int ammouSize, float reloadTime)
    {
        _ammouSize = ammouSize;
        _cooldownReload = reloadTime;
    }

    public void RechargeTurret()
    {
        _currentCoutBullet = _ammouSize;
        ClipSizeChanged?.Invoke();
    }

    public void DestroyTurret()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ShootingControl();
    }

    private void FindTarget()
    {
        _enemies.Clear();
        _currentTarget = null;
        transform.rotation = Quaternion.identity;
        var colliders = Physics.OverlapSphere(transform.position, SearchRadius);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Enemy>(out Enemy enemy))
            {
                float distanceToTarget = Vector3.Distance(enemy.transform.position, transform.position);

                if (distanceToTarget <= SearchRadius)
                    _enemies.Add(distanceToTarget, enemy);
            }
        }

        if (_enemies.Count == 0)
        {
            return;
        }
        else
        {
            _currentTarget = _enemies.OrderBy(distance => distance.Key).First().Value;
            _currentTarget.GetCrosshairs();
            CorountineStart(Shooting());
        }
    }

    private void LookAtEnemy()
    {
        Vector3 lookDir = _currentTarget.transform.position - transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
    }

    private void ShootingControl()
    {
        if (HaveEnemy())
        {
            LookAtEnemy();
        }
        else
        {
            StopCoroutine(Shooting());
            FindTarget();
            return;
        }
    }

    private void InstantiateBullet(int shootPointIndex, Enemy target)
    {
        Bullet bullet;

        if (_poolBullet.TryPoolObject(out IPoolObject pollBullet))
        {
            bullet = pollBullet as Bullet;
            bullet.transform.position = _shootPoints[shootPointIndex].transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = Instantiate(_bullet, _shootPoints[shootPointIndex].transform.position, transform.rotation);
            _poolBullet.InstantiatePoolObject(bullet);
        }

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * SpeedBullet, ForceMode.VelocityChange);
        _animator.SetTrigger("Shoot");
        _shootPoints[shootPointIndex].PlayParticle();
        _currentCoutBullet--;
        ClipSizeChanged?.Invoke();
        Shoted?.Invoke();
    }

    private IEnumerator Shooting()
    {
        int curretPoint = 0;
        
        while (curretPoint <= _shootPoints.Length)
        {
            yield return new WaitForSeconds(_delayShot);

            if (HaveEnemy())
            {
                for (int j = 0; j < _shootPoints.Length; j++)
                {
                    if (_currentCoutBullet <= 0)
                    {
                        CorountineStart(Recharge());
                    }
                    else
                    {
                        InstantiateBullet(j, _currentTarget);
                        curretPoint = j;
                    }
                }
            }
            else
            {
                FindTarget();
            }
        }

        curretPoint = 0;
    }

    private IEnumerator Recharge()
    {
        AmmouRecharging?.Invoke();
        yield return new WaitForSeconds(_cooldownReload);
        _currentCoutBullet = _ammouSize;
        ClipSizeChanged?.Invoke();

        if (HaveEnemy())
            CorountineStart(Shooting());
        else
            FindTarget();
    }

    private void CorountineStart(IEnumerator corontine)
    {
        if (_corontine != null)
            StopCoroutine(_corontine);

        _corontine = StartCoroutine(corontine);
    }

    private bool HaveEnemy()
    {
        if (_currentTarget == null || _currentTarget.isActiveAndEnabled == false)
            return false;
        else
            return true;
    }
}