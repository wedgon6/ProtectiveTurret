using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLvl_5 : BaseTurret
{
    private float _delayShotLvl_5 = 0.15f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _delayShot = _delayShotLvl_5;
    }
}
