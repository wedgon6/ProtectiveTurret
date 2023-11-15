using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int _currentMoney = 0;

    public int CurrentMoney => _currentMoney;
    public event Action ChengetMoney;

    public void AddMoney(int money)
    {
        if(money < 0)
            return;

        _currentMoney += money;
        ChengetMoney?.Invoke();
    }
}
