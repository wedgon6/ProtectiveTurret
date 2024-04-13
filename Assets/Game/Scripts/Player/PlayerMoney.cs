using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int _currentMoney = 0;

    public int CurrentMoney => _currentMoney;
    public event Action MoneyChanged;

    public void SetMoney(int currentMoneu)
    {
        _currentMoney = currentMoneu;
        MoneyChanged?.Invoke();
    }

    public void AddMoney(int money)
    {
        if(money < 0)
            return;

        _currentMoney += money;
        MoneyChanged?.Invoke();
    }

    public void ReduceMoney(int money)
    {
        if (money < 0)
            return;

        if (money > _currentMoney)
            return;

        _currentMoney -= money;
        MoneyChanged?.Invoke();
    }
}
