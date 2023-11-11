using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractTurret : MonoBehaviour
{
    protected const float SearchRadius = 20f;
    protected const float SpeedBullet = 10f;

    protected Enemy _currentTarget;
    protected int _clipSize;
    protected Coroutine _corontine;
    //protected event Action ClipSizeChanged;


    public int CurrentSizeClip => _clipSize;

    public abstract void Shoot(Transform[] shootPoints);

    protected virtual void FindTarget()
    {
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

    protected virtual IEnumerator Shooting()
    {
        yield return null;
    }

    protected virtual IEnumerator Recharge()
    {
        yield return null;
    }
}
