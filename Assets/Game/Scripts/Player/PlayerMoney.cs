using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int _currentMoney = 0;

    public int CurrentMoney => _currentMoney;
    public Action onChengetMoney;

    public void AddMoney(int money)
    {
        if(money < 0)
            return;

        _currentMoney += money;
        onChengetMoney?.Invoke();
    }

    public void ReduceMoney(int money)
    {
        if (money < 0)
            return;

        if (money > _currentMoney)
            return;

        _currentMoney -= money;
        onChengetMoney?.Invoke();
    }
}
