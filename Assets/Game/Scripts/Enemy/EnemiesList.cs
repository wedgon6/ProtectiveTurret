using UnityEngine;

[System.Serializable]
public class EnemiesList
{
    private const int EasyWaveIndex = 3;
    private const int MidWaveIndex = 7;

    [SerializeField] private Enemy _standartEnemy;
    [SerializeField] private Enemy _fastEnemy;
    [SerializeField] private Enemy _bigEnemy;

    [SerializeField] private int[] _indexsStandartEnemy = {0, 1, 2, 3, 4, 5};
    [SerializeField] private int[] _indexsFastEnemy = { 6, 7, 8, };
    [SerializeField] private int[] _indexsBigEnemy = {9, 10};

    public EnemiesList(Enemy standartEnemy, Enemy fastEnemy, Enemy bigEnemy)
    {
        _standartEnemy = standartEnemy;
        _fastEnemy = fastEnemy;
        _bigEnemy = bigEnemy;
    }

    public Enemy StandartEnemy => _standartEnemy;
    public Enemy FastEnemy => _fastEnemy;
    public Enemy BigEnemy => _bigEnemy;


    public Enemy GetEnemy(int complexityWave)
    {
        Enemy enemy;
        int enemyIndex;

        if (complexityWave <= EasyWaveIndex)
        {
            enemyIndex = Random.Range(0, 5);

            foreach (var index in _indexsStandartEnemy)
            {
                if (index == enemyIndex)
                    return enemy = _standartEnemy;
            }
        }
        else if(complexityWave > EasyWaveIndex && complexityWave <= MidWaveIndex)
        {
            enemyIndex = Random.Range(0, 9);

            foreach (var index in _indexsStandartEnemy)
            {
                if (index == enemyIndex)
                    return enemy = _standartEnemy;
            }

            foreach (var index in _indexsFastEnemy)
            {
                if (index == enemyIndex)
                    return enemy = _fastEnemy;
            }
        }
        else if(complexityWave > MidWaveIndex)
        {
            enemyIndex = Random.Range(0, 11);

            foreach (var index in _indexsStandartEnemy)
            {
                if (index == enemyIndex)
                    return enemy = _standartEnemy;
            }

            foreach (var index in _indexsFastEnemy)
            {
                if (index == enemyIndex)
                    return enemy = _fastEnemy;
            }   
            
            foreach (var index in _indexsBigEnemy)
            {
                if (index == enemyIndex)
                    return enemy = _bigEnemy;
            }
        }

        return null;
    }
}
