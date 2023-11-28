using UnityEngine;

public class LevelProgressPresenter : MonoBehaviour
{
    private const bool IsLooseGame = false;
    private const bool IsWinGame = true;

    [SerializeField] private GameResultPanels _gameResultPanels;

    [Header("Loose")]
    [SerializeField] private ReadLine _readLine;

    [Header("Win")]
    [SerializeField] private EnemyPresenter _enemyiesPresenter;

    private void OnEnable()
    {
        _readLine.onLooseGame += OnShowLoosePanel;
        _enemyiesPresenter.OnAllEnemiesDie += OnShowWinPanel;
    }

    private void OnDisable()
    {
        _readLine.onLooseGame -= OnShowLoosePanel;
        _enemyiesPresenter.OnAllEnemiesDie -= OnShowWinPanel;
    }

    private void OnShowLoosePanel()
    {
        _gameResultPanels.ShowResult(IsLooseGame);
    }

    private void OnShowWinPanel()
    {
        _gameResultPanels.ShowResult(IsWinGame);
    }
}
