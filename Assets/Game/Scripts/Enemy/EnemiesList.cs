using UnityEngine;

[System.Serializable]
public class EnemiesList
{
    [SerializeField] private Enemy _standartEnemy;
    [SerializeField] private Enemy _fastEnemy;
    [SerializeField] private Enemy _bigEnemy;

    public Enemy StandartEnemy => _standartEnemy;
    public Enemy FastEnemy => _fastEnemy;
    public Enemy BigEnemy => _bigEnemy;

    public EnemiesList(Enemy standartEnemy, Enemy fastEnemy, Enemy bigEnemy)
    {
        _standartEnemy = standartEnemy;
        _fastEnemy = fastEnemy;
        _bigEnemy = bigEnemy;
    }
}
