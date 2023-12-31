using System;
using UnityEngine;

public abstract class PlayerAbillity : MonoBehaviour
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _icon;
    [SerializeField] protected float _multiplier;
    [SerializeField] private int _startPrice;

    private int _currentLvl = 1;
    
    protected int _currentPrice;

    public string Lable => _lable;
    public Sprite Icon => _icon;
    public int Price => _currentPrice;
    public int CurrentLvl => _currentLvl;

    public Action OnPriceChenget;

    public void Initialize()
    {
        _currentPrice = _startPrice;
    }

    public abstract void Buy(Player player);
}
