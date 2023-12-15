using System;
using UnityEngine;

public class ReloadAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        _currentPrice = (int)Math.Round(_currentPrice * _multiplier, 0);
        OnPriceChenget?.Invoke();
        Debug.Log("апнул скорость перезарядки");
    }
}
