using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLvl_4 : BaseTurret
{
    private float _delayShotLvl_4 = 0.20f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _delayShot = _delayShotLvl_4;
    }
}
