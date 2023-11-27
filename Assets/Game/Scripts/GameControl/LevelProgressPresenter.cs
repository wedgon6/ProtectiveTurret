using UnityEngine;

public class LevelProgressPresenter : MonoBehaviour
{
    [Header("Loose")]
    [SerializeField] private ReadLine _readLine;
    [SerializeField] private LooseGamePanel _loosePanel;

    [Header("Win")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private WinGamePanel _winPanel;

    private void OnEnable()
    {
        _readLine.onLooseGame += OnLooseGamePanel;
        _enemySpawner.onAllEnemyDie += OnWinGamePanel;
    }

    private void OnDisable()
    {
        _readLine.onLooseGame -= OnLooseGamePanel;
        _enemySpawner.onAllEnemyDie -= OnWinGamePanel;
    }

  
    private void OnLooseGamePanel()
    {
        _loosePanel.SetActive(true);
    }

    private void OnWinGamePanel()
    {
        _winPanel.SetActive(true);
    }
}
