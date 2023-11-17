using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractTurret : MonoBehaviour
{
    [SerializeField] protected PoolBullet _poolBullet;
    [SerializeField] protected Transform[] _shootPoints;
    [SerializeField] protected Bullet _bullet;

    protected const float SearchRadius = 20f;
    protected const float SpeedBullet = 10f;

    protected Enemy _currentTarget;
    protected int _clipSize;
    protected int _currentCoutBullet;
    protected Coroutine _corontine;
    protected int _cooldown;

    public Action onClipSizeChanged;
    public int CurrentSizeClip => _currentCoutBullet;

    protected virtual void FindTarget()
    {
        transform.rotation = Quaternion.identity;
        var colliders = Physics.OverlapSphere(transform.position, SearchRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Enemy>(out Enemy enemy))
            {
                _currentTarget = enemy;
                return;
            }
        }
    }

    protected virtual void LookAtEnemy()
    {
        Vector3 lookDir = _currentTarget.transform.position - transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
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
        }
        else
        {
            bullet = Instantiate(_bullet, _shootPoints[shootPoint].position, _shootPoints[shootPoint].rotation);
            bullet.Initialize(SpeedBullet, _poolBullet);
        }
    }

    protected virtual IEnumerator Shooting()
    {
        int curretPoint = 0;

        while(curretPoint <= _shootPoints.Length)
        {
            if (_currentCoutBullet == 0)
                CorountineStart(Recharge());

            for (int j = 0; j < _shootPoints.Length; j++)
            {
                yield return new WaitForSeconds(0.5f);
                InstantiateBullet(j);
                _currentCoutBullet--;
                onClipSizeChanged?.Invoke();

                curretPoint = j;
            }
        }

        curretPoint = 0;
    }

    protected virtual IEnumerator Recharge()
    {
        yield return new WaitForSeconds(_cooldown);
        _currentCoutBullet = _clipSize;
        onClipSizeChanged?.Invoke();
        CorountineStart(Shooting());
    }

    protected virtual void CorountineStart(IEnumerator corontine)
    {
        if (_corontine != null)
            StopCoroutine(_corontine);

        _corontine = StartCoroutine(corontine);
    }
}
