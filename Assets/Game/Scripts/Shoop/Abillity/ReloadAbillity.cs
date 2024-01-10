using System;
using UnityEngine;

public class ReloadAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        player.DiscountCooldownReload();
        _currentPrice = (int)Math.Round(_currentPrice * _multiplier, 0);
        _currentLvl++;
        OnPriceChenget?.Invoke();
        OnLvlChenget?.Invoke();
    }
}
