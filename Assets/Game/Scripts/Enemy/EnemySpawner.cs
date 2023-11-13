using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IPooling
{
    [SerializeField] private GameObject _target;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoint;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public Queue<IPoolObject> PoolObjects => new Queue<IPoolObject>();

    private void Awake()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateObject();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
            {
                StartCoroutine(StartNextWave());
            }

            _currentWave = null;
        }
    }

    private void InitializeEnemy()
    {
        int currentSpawnPont = Random.Range(0, _spawnPoint.Length);

        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint[currentSpawnPont].position, _spawnPoint[currentSpawnPont].rotation, 
            _spawnPoint[currentSpawnPont]).GetComponent<Enemy>();
        enemy.Initialize(_target);
        PoolObjects.Enqueue(enemy);
        Debug.Log($"{PoolObjects.Count}");

    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private IEnumerator StartNextWave()
    {
        int waitTime = 5;
        yield return new WaitForSeconds(waitTime);
        NextWave();
    }

    public void InstantiateObject()
    {
        if(PoolObjects.Count == 0)
        {
            InitializeEnemy();
            return;
        }

        var spawnOnject = PoolObjects.Dequeue();
        PullEnemy(spawnOnject);
    }

    private void PullEnemy(IPoolObject poolObject)
    {
        Enemy enemy = poolObject as Enemy;
        enemy.transform.position = _spawnPoint[0].position;
        enemy.transform.rotation = _spawnPoint[0].rotation;
        enemy.gameObject.SetActive(true);
        PoolObjects.Enqueue(enemy);
        Debug.Log($"{PoolObjects.Count}");
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public float Delay;
    public int Count;
}
