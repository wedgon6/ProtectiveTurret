using System;
using Lean.Localization;
using TMPro;
using UnityEngine;

public abstract class PlayerAbillity : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _multiplier;
    [SerializeField] private int _startPrice;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localized;

    private int _currentLvl = 1;
    private int _currentPrice;

    public event Action PriceChanged;
    public event Action LvlChanged;

    protected float Multiplier => _multiplier;
    protected int CurrentLvl => _currentLvl;
    protected int CurrentPrice => _currentPrice;

    public string Lable => _lable.text;
    public Sprite Icon => _icon;
    public int Price => _currentPrice;
    public int CurrentLvlAbillity => _currentLvl;

    public void Initialize(int currentLvL = 1)
    {
        _localized.UpdateTranslation(LeanLocalization.GetTranslation(_localized.TranslationName));
        _currentPrice = _startPrice;
        _currentLvl = currentLvL;
        PriceChanged?.Invoke();
        LvlChanged?.Invoke();
    }

    public void GetCloudData(int currentLvl, int currentPrice)
    {
        _currentLvl = currentLvl;
        _currentPrice = currentPrice;
        PriceChanged?.Invoke();
        LvlChanged?.Invoke();
    }

    public virtual void Buy(Player player)
    {
        _currentPrice = (int)Math.Round(CurrentPrice * Multiplier, 0);
        _currentLvl++;
        PriceChanged?.Invoke();
        LvlChanged?.Invoke();
    }
}