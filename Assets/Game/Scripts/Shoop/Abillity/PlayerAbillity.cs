using System;
using UnityEngine;

public abstract class PlayerAbillity : MonoBehaviour
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _icon;
    [SerializeField] protected float _multiplier;
    [SerializeField] private int _startPrice;

    protected int _currentLvl = 1;
    
    protected int _currentPrice;

    public string Lable => _lable;
    public Sprite Icon => _icon;
    public int Price => _currentPrice;
    public int CurrentLvl => _currentLvl;

    public Action OnPriceChenget;
    public Action OnLvlChenget;

    public void Initialize(int currentPrice = 1, int currentLvL = 1)
    {
        _currentPrice = _startPrice;
        _currentLvl = currentLvL;
    }

    public abstract void Buy(Player player);
}
