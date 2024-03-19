using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour
{
    [SerializeField] private Transform _containet;
    [SerializeField] private ElementPlayer _leaderboardElementPrefab;

    private List<ElementPlayer> _spawnedElements = new List<ElementPlayer>();

    public void ConstructLeaderboard(List<DataPlayer> leaderboardPlayer)
    {
        ClearLeaderboard();
        Debug.Log("LB View player count----"+leaderboardPlayer.Count);
        foreach (DataPlayer player in leaderboardPlayer)
        {
            ElementPlayer elementPlayerInstance = Instantiate(_leaderboardElementPrefab, _containet);
            elementPlayerInstance.Initialize(player.Name, player.Rank, player.Score);
            Debug.Log($"{player.Name}, {player.Rank}, {player.Score} -- View Player Data");
            _spawnedElements.Add(elementPlayerInstance);
        }
        Debug.Log(_spawnedElements.Count);
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
        {
            Destroy(element.gameObject);
            Debug.Log("Destroy elemet leaderboard");
        }

        _spawnedElements = new List<ElementPlayer>();
    }
}
