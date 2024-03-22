using Lean.Localization;
using System;
using TMPro;
using UnityEngine;

public abstract class PlayerAbillity : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Sprite _icon;
    [SerializeField] protected float _multiplier;
    [SerializeField] private int _startPrice;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localized;

    protected int _currentLvl = 1;
    protected int _currentPrice;

    public string Lable => _lable.text;
    public Sprite Icon => _icon;
    public int Price => _currentPrice;
    public int CurrentLvl => _currentLvl;

    public Action OnPriceChenget;
    public Action OnLvlChenget;

    public void Initialize(int currentLvL = 1)
    {
        _localized.UpdateTranslation(LeanLocalization.GetTranslation(_localized.TranslationName));
        _currentPrice = _startPrice;
        _currentLvl = currentLvL;
        OnPriceChenget?.Invoke();
        OnLvlChenget?.Invoke();
    }

    public void GetCloudData(int currentLvl, int currentPrice)
    {
        _currentLvl = currentLvl;
        _currentPrice = currentPrice;
        OnPriceChenget?.Invoke();
        OnLvlChenget?.Invoke();
    }

    public abstract void Buy(Player player);
}
