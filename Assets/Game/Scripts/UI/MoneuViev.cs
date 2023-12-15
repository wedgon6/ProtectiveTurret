using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneuViev : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentMoney;
    [SerializeField] private PlayerMoney _player;

    private void Start()
    {
        _currentMoney.text = _player.CurrentMoney.ToString();
    }

    private void OnEnable()
    {
        _player.OnChengetMoney += OnAmountChanged;
    }

    private void OnDisable()
    {
        _player.OnChengetMoney -= OnAmountChanged;
    }

    private void OnAmountChanged()
    {
        _currentMoney.text = _player.CurrentMoney.ToString();
    }
}
