using System;
using UnityEngine;

public class AmmoCountAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        _currentPrice = (int)Math.Round(_currentPrice * _multiplier, 0);
        OnPriceChenget?.Invoke();
        Debug.Log("����� ������");
    }
}
