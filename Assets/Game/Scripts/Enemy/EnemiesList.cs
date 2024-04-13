using UnityEngine;

[System.Serializable]
public class EnemiesList
{
    private const int EasyWaveIndex = 3;
    private const int MidWaveIndex = 7;

    [SerializeField] private Enemy _standartEnemy;
    [SerializeField] private Enemy _fastEnemy;
    [SerializeField] private Enemy _bigEnemy;

    [SerializeField] private int _maxIndexStandartEnemy = 5;
    [SerializeField] private int _maxIndexFastEnemy = 9;
    [SerializeField] private int _maxIndexBigEnemy = 11;

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
        int enemyIndex;

        if (complexityWave <= EasyWaveIndex)
        {
            return _standartEnemy;
        }
        else if (complexityWave > EasyWaveIndex && complexityWave <= MidWaveIndex)
        {
            enemyIndex = Random.Range(0, _maxIndexFastEnemy);

            if (enemyIndex > _maxIndexStandartEnemy)
                return _fastEnemy;
            else
                return _standartEnemy;
        }
        else if (complexityWave > MidWaveIndex)
        {
            enemyIndex = Random.Range(0, _maxIndexBigEnemy);

            if (enemyIndex > _maxIndexFastEnemy)
                return _bigEnemy;
            else if (enemyIndex <= _maxIndexFastEnemy && enemyIndex > _maxIndexStandartEnemy)
                return _fastEnemy;
            else
                return _standartEnemy;
        }

        return null;
    }
}