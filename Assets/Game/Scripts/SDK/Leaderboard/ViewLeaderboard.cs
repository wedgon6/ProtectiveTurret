using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour
{
    [SerializeField] private Transform _containet;
    [SerializeField] private ElementPlayer _leaderboardElementPrefab;

    private List<ElementPlayer> _spawnedElements = new();

    public void ConstructLeaderboard(List<DataPlayer> leaderboardPlayer)
    {
        ClearLeaderboard();

        foreach (DataPlayer player in leaderboardPlayer)
        {
            ElementPlayer elementPlayerInstance = Instantiate(_leaderboardElementPrefab, _containet);
            elementPlayerInstance.Initialize(player.Name, player.Rank, player.Score);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
        {
            Destroy(element);
        }

        _spawnedElements = new List<ElementPlayer>();
    }
}
