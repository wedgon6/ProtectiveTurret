using System.Collections.Generic;
using UnityEngine;

public class Shoop : MonoBehaviour
{
    [SerializeField] private List<PlayerAbillity> _abillities;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerLevel _playerLvl;  
    [SerializeField] private ShoopView _template;
    [SerializeField] private GameObject _itemContainer;

    public List<PlayerAbillity> Abillities => _abillities;

    public void InitializeShop()
    {
        for (int i = 0; i < _abillities.Count; i++)
        {
            AddItem(_abillities[i]);
        }
    }

    public void SetAbillitiesData(List<int> abilitiesLvl, List<int> abilitiesPrise)
    {
        if (abilitiesLvl == null || abilitiesPrise == null)
            return;

        if (abilitiesLvl.Count == 0 || abilitiesPrise.Count == 0)
            return;

        for (int i = 0; i < _abillities.Count; i++)
        {
            _abillities[i].GetCloudData(abilitiesLvl[i], abilitiesPrise[i]);
        }
    }

    private void AddItem(PlayerAbillity abillity)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClicked += OnSellButtonClick;
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
