using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseTurret : MonoBehaviour
{
    protected const float SearchRadius = 20f;
    protected const float SpeedBullet = 15f;

    [SerializeField] protected Transform[] _shootPoints;
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected PoolBullet _poolBullet;

    private Dictionary<float, Enemy> _enemies = new Dictionary<float, Enemy>();
    
    protected Enemy _currentTarget;
    protected Coroutine _corontine;
    protected Animator _animator;

    protected int _ammouSize;
    protected int _currentCoutBullet;
    protected float _cooldownReload;
    protected float _delayShot = 0.25f;

    public Action OnClipSizeChanged;
    public Action OnShot;
    public Action OnRechargeAmmou;

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
        OnClipSizeChanged?.Invoke();
    }

    public void DestroyTurret()
    {
        Destroy(gameObject);
    }

    protected virtual void FindTarget()
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
            _currentTarget = _enemies.OrderByDescending(distance => distance.Key).First().Value;
            _currentTarget.GetCrosshairs();
            CorountineStart(Shooting());
        }
    }

    protected virtual void LookAtEnemy()
    {
        Vector3 lookDir = _currentTarget.transform.position - transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
    }

    protected virtual void ShootingControl()
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

    protected virtual void InstantiateBullet(int shootPoint, Enemy target)
    {
        Bullet bullet;

        if(_poolBullet.TryPoolObject(out IPoolObject pollBullet))
        {
            bullet = pollBullet as Bullet;
            bullet.transform.position = _shootPoints[shootPoint].position;
            bullet.transform.rotation = transform.rotation;
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = Instantiate(_bullet, _shootPoints[shootPoint].position, transform.rotation);
        }

        bullet.Initialize(SpeedBullet, _poolBullet, target);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * SpeedBullet, ForceMode.VelocityChange);
        _animator.SetTrigger("Shoot");
        _currentCoutBullet--;
        OnClipSizeChanged?.Invoke();
        OnShot?.Invoke();
    }

    protected virtual IEnumerator Shooting()
    {
        int curretPoint = 0;

        while(curretPoint <= _shootPoints.Length)
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

    protected virtual IEnumerator Recharge()
    {
        OnRechargeAmmou?.Invoke();
        yield return new WaitForSeconds(_cooldownReload);
        _currentCoutBullet = _ammouSize;
        OnClipSizeChanged?.Invoke();

        if (HaveEnemy())
            CorountineStart(Shooting());
        else
            FindTarget();
    }

    protected virtual void CorountineStart(IEnumerator corontine)
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
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ShootingControl();
    }
}
