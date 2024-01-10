using System.Collections.Generic;
using UnityEngine;

public class Shoop : MonoBehaviour
{
    [SerializeField] private List<PlayerAbillity> _abillities;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerLevel _playerLvl;  
    [SerializeField] private ShoopView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _abillities.Count; i++)
        {
            AddItem(_abillities[i]);
        }
    }

    private void AddItem(PlayerAbillity abillity)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.OnSellButtonClick += OnSellButtonClick;
        view.Render(abillity);
    }

    private void OnSellButtonClick(PlayerAbillity abillity, ShoopView view)
    {
        TrySellAbillity(abillity, view);
    }

    private void TrySellAbillity(PlayerAbillity abillity, ShoopView view)
    {
        if (abillity.Price > _player.CurrentMoney)
            return;

        if (abillity.Price <= _player.CurrentMoney)
        {
            _player.ReduceMoney(abillity.Price);
            abillity.Buy(_player);
            _playerLvl.AddExperience();
        }
    }
}
