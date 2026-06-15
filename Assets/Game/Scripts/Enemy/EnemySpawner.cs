using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
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
        Instantiate(_enemyPrefab, _spawnPoints[spawnpoint].position, Quaternion.identity);
    }
}
