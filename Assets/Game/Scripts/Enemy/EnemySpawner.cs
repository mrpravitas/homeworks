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

    private bool _firstWaveSpawned = false;

    public static event Action OnFirstWaveSpawned;

    private void Awake()
    {
        Debug.Log($"New wave of enemy every {_waveInterval} seconds!");        
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
            enemyToSpawn = _enemyPrefab2;
        }
        else
        {
            enemyToSpawn = _enemyPrefab;
        }

        Instantiate(enemyToSpawn, _spawnPoints[spawnpoint].position, Quaternion.identity);
    }
}
