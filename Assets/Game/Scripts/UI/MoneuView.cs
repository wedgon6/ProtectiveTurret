using ProtectiveTurret.PlayerScripts;
using TMPro;
using UnityEngine;

namespace ProtectiveTurret.UI
{
    public class MoneuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentMoney;
        [SerializeField] private PlayerMoney _player;

        private void Start()
        {
            _currentMoney.text = _player.CurrentMoney.ToString();
        }

        private void OnEnable()
        {
            _player.MoneyChanged += OnAmountChanged;
        }

        private void OnDisable()
        {
            _player.MoneyChanged -= OnAmountChanged;
        }

        private void OnAmountChanged()
        {
            _currentMoney.text = _player.CurrentMoney.ToString();
        }
    }
}