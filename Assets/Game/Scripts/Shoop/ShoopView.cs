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

    public event Action<PlayerAbillity, ShoopView> SellButtonClicked;

    public void Render(PlayerAbillity abillity)
    {
        _abillity = abillity;
        _abillity.Initialize();
        _lable.text = abillity.Lable;
        _price.text = abillity.Price.ToString();
        _icon.sprite = abillity.Icon;
        _currentLvl.text = abillity.CurrentLvlAbillity.ToString();
        _abillity.PriceChanged += OnPriceChenged;
        _abillity.LvlChanged += OnLvlAbillityChenged;
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

    private void OnLvlAbillityChenged()
    {
        _currentLvl.text = _abillity.CurrentLvlAbillity.ToString();
    }

    private void OnButtonClick()
    {
        SellButtonClicked?.Invoke(_abillity, this);
    }
}
