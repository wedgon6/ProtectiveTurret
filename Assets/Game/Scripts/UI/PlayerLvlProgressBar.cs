using UnityEngine;
using UnityEngine.UI;

public class PlayerLvlProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private PlayerLevel _playerLvl;

    private void OnEnable()
    {
        _playerLvl.OnPlayerExpirianceChanget += OnEnemiesDeadCountChenget;
    }

    private void OnDisable()
    {
        _playerLvl.OnPlayerExpirianceChanget -= OnEnemiesDeadCountChenget;
    }

    private void OnEnemiesDeadCountChenget(int currentValue, int totalValue)
    {
        float amount = (float)currentValue / (float)totalValue;
        _bar.value = amount;
    }
}
