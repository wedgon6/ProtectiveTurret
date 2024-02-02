using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [SerializeField] private Enemy _template;
    [SerializeField] private int _count;

    public Enemy Template => _template;
    public int Count => _count; 

    public EnemyWave(Enemy template, int count)
    {
        _template = template;
        _count = count;
    }
}
