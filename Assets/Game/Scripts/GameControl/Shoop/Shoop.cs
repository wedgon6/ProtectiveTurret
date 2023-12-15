using System.Collections.Generic;
using UnityEngine;

public class Shoop : MonoBehaviour
{
    [SerializeField] private List<PlayerAbillity> _abillities;
    [SerializeField] private Player _player;
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
        TrySellWeapon(abillity, view);
    }

    private void TrySellWeapon(PlayerAbillity abillity, ShoopView view)
    {
        if (abillity.Price > _player.CurrentMoney)
            Debug.Log("нет денег");

        if (abillity.Price <= _player.CurrentMoney)
        {
            _player.ReduceMoney(abillity.Price);
            Debug.Log($"{abillity.Price} - отнял деньги");
            abillity.Buy(_player);
        }
    }
}
