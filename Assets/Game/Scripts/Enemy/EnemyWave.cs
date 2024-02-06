using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    private List<Enemy> _template;
    private int _currentIndex = -1;

    public EnemyWave(List<Enemy> template)
    {
        _template = template;
    }

    public List<Enemy> Template => _template;

    public Enemy GetNextEnemy()
    {
        _currentIndex++;

        if (_currentIndex > _template.Count-1)
            return null;

        return _template[_currentIndex];
    }
}
