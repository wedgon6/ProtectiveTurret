using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseTurret : MonoBehaviour
{
    [SerializeField] protected Transform[] _shootPoints;
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected PoolBullet _poolBullet;

    private Dictionary<float, Enemy> _enemies = new Dictionary<float, Enemy>();
    
    protected const float SearchRadius = 20f;
    protected const float SpeedBullet = 10f;
    
    protected Enemy _currentTarget;
    protected Coroutine _corontine;
    protected Animator _animator;

    protected int _clipSize = 50;
    protected float _cooldown = 1.5f;
    protected int _currentCoutBullet;

    public Action OnClipSizeChanged;

    public int CurrentCountBullet => _currentCoutBullet;

    public void RechargeTurret()
    {
        _currentCoutBullet = _clipSize;
        OnClipSizeChanged?.Invoke();
        Debug.Log("Перезарядка турели");
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
            _animator.SetTrigger("Shoot");
        }
        else
        {
            bullet = Instantiate(_bullet, _shootPoints[shootPoint].position, transform.rotation);
            bullet.Initialize(SpeedBullet, _poolBullet, target);
            _animator.SetTrigger("Shoot");
        }

        _currentCoutBullet--;
        OnClipSizeChanged?.Invoke();
        Debug.Log("отнят пули");
    }

    protected virtual IEnumerator Shooting()
    {
        int curretPoint = 0;

        while(curretPoint <= _shootPoints.Length)
        {
            yield return new WaitForSeconds(0.25f);

            if (HaveEnemy())
            {
                for (int j = 0; j < _shootPoints.Length; j++)
                {

                    if (_currentCoutBullet == 0)
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
        yield return new WaitForSeconds(_cooldown);
        _currentCoutBullet = _clipSize;
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
