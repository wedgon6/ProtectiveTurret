using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [SerializeField] private Enemy _template;
    [SerializeField] private float _delay;
    [SerializeField] private int _count;

    public Enemy Template => _template;
    public float Delay => _delay;
    public int Count => _count; 

    public EnemyWave(Enemy template, float delay, int count)
    {
        _template = template;
        _delay = delay;
        _count = count;
    }
}
