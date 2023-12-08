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

    protected int _clipSize;
    protected int _currentCoutBullet;
    protected float _cooldown;

    public Action onClipSizeChanged;

    public int CurrentSizeClip => _currentCoutBullet;

    public void RechargeTurret()
    {
        _currentCoutBullet = _clipSize;
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
        if (CanEnemy())
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

    protected virtual void InstantiateBullet(int shootPoint)
    {
        Bullet bullet;

        if(_poolBullet.TryPoolObject(out IPoolObject pollBullet))
        {
            bullet = pollBullet as Bullet;
            bullet.transform.position = _shootPoints[shootPoint].position;
            bullet.transform.rotation = _shootPoints[shootPoint].rotation;
            bullet.gameObject.SetActive(true);
            _animator.SetTrigger("Shoot");
        }
        else
        {
            bullet = Instantiate(_bullet, _shootPoints[shootPoint].position, _shootPoints[shootPoint].rotation);
            bullet.Initialize(SpeedBullet, _poolBullet);
            _animator.SetTrigger("Shoot");
        }
    }

    protected virtual IEnumerator Shooting()
    {
        int curretPoint = 0;

        while(curretPoint <= _shootPoints.Length)
        {
            yield return new WaitForSeconds(0.25f);

            if (CanEnemy())
            {
                for (int j = 0; j < _shootPoints.Length; j++)
                {

                    if (_currentCoutBullet == 0)
                    {
                        CorountineStart(Recharge());
                    }
                    else
                    {
                        InstantiateBullet(j);
                        _currentCoutBullet--;
                        onClipSizeChanged?.Invoke();

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
        onClipSizeChanged?.Invoke();

        if (CanEnemy())
        {
            CorountineStart(Shooting());
        }
        else
        {
            FindTarget();
        }
    }

    protected virtual void CorountineStart(IEnumerator corontine)
    {
        if (_corontine != null)
            StopCoroutine(_corontine);

        _corontine = StartCoroutine(corontine);
    }

    private bool CanEnemy()
    {
        if (_currentTarget == null || _currentTarget.isActiveAndEnabled == false)
            return false;
        else
            return true;
    }
}
