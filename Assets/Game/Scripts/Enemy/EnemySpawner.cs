using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] [Range(0f, 1f)] private float _enemy2Chance;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _waveInterval;
    [SerializeField] private GameObject _enemyProjectilePrefab;

    private bool _firstWaveSpawned = false;

    private EnemyPool _pool1;
    private EnemyPool _pool2;
    private ProjectilePool _enemyProjectilePool;

    public static event Action OnFirstWaveSpawned;

    private void Awake()
    {
        Debug.Log($"New wave of enemy every {_waveInterval} seconds!");

        _enemyProjectilePool = new ProjectilePool(_enemyProjectilePrefab, 2);

        _pool1 = new EnemyPool(_enemyPrefab, 2);
        _pool2 = new EnemyPool(_enemyPrefab2, 2);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnWaves());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waveInterval);
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        if (!_firstWaveSpawned)
        {
            _firstWaveSpawned = true;
            OnFirstWaveSpawned.Invoke();
        }

        int firstPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);
        int secondPoint;
        do
        {
            secondPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);
        } 
        while (firstPoint == secondPoint);

        SpawnEnemy(firstPoint);
        SpawnEnemy(secondPoint);
    }

    private void SpawnEnemy(int spawnpoint)
    {
        GameObject enemyToSpawn;

        if (_enemyPrefab2 != null && UnityEngine.Random.Range(0f, 1f) < _enemy2Chance)
        {
            enemyToSpawn = _pool2.Get();
        }
        else
        {
            enemyToSpawn = _pool1.Get();
        }

        enemyToSpawn.transform.position = _spawnPoints[spawnpoint].position;
        enemyToSpawn.transform.rotation = Quaternion.identity;

        EnemyShooting shooting = enemyToSpawn.GetComponent<EnemyShooting>();
        shooting.SetPool(_enemyProjectilePool);
    }
}
