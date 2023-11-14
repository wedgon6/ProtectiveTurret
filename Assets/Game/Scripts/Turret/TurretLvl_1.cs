using System;
using System.Collections;
using UnityEngine;

public class TurretLvl_1 : AbstractTurret
{
    [SerializeField] private Transform[] _shootPoints;
    [SerializeField] private Bullet _bullet;

    private float _currentTime = 4;
    protected int _clipSizeLvl1 = 5;

    public event Action ClipSizeChanged;

    private void Awake()
    {
        _clipSize = _clipSizeLvl1;
    }

    private void FixedUpdate()
    {
        if(_currentTarget != null && _currentTarget.isActiveAndEnabled != false)
        {
            LookAtEnemy();
        }
        else
        {
            FindTarget();
        }
    }

    protected override void FindTarget()
    {
        base.FindTarget();
        CorountineStart(Shooting());
    }

    public override void Shoot(Transform[] shotPoint)
    {
        LookAtEnemy();

        if (_currentTime >= 3)
        {
            Instantiate(_bullet, shotPoint[0].position, shotPoint[0].rotation).Initialize(SpeedBullet);
            _currentTime = 0;
        }
        else
        {
            _currentTime += Time.fixedDeltaTime;
        }
    }

    protected override IEnumerator Shooting()
    {
        for(int i = _clipSize; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(_bullet, _shootPoints[0].position, _shootPoints[0].rotation).Initialize(SpeedBullet);
            _clipSize--;
            ClipSizeChanged?.Invoke();

            if (_clipSize == 0)
                CorountineStart(Recharge());
        }
    }

    protected override IEnumerator Recharge()
    {
        yield return new WaitForSeconds(3f);
        _clipSize = _clipSizeLvl1;
        ClipSizeChanged?.Invoke();
        CorountineStart(Shooting());

    }

    protected void CorountineStart(IEnumerator corontine)
    {
        if (_corontine != null)
            StopCoroutine(_corontine);

        _corontine = StartCoroutine(corontine);
    }
}
