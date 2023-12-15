using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoopView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _currentLvl;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private PlayerAbillity _abillity;

    public Action<PlayerAbillity, ShoopView> OnSellButtonClick;

    public void Render(PlayerAbillity abillity)
    {
        _abillity = abillity;
        _abillity.Initialize();
        _lable.text = abillity.Lable;
        _price.text = abillity.Price.ToString();
        _icon.sprite = abillity.Icon;
        _currentLvl.text = abillity.CurrentLvl.ToString();
        _abillity.OnPriceChenget += OnPriceChenged;
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnPriceChenged()
    {
        _price.text = _abillity.Price.ToString();
    }

    private void OnButtonClick()
    {
        OnSellButtonClick?.Invoke(_abillity, this);
    }
}
