using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _initialPoolSize;

    private Pool<Enemy> _pool;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _pool = new Pool<Enemy>(_enemyPrefab, _initialPoolSize);
        G.EnemyPool = _pool;
        _wait = new WaitForSeconds(_spawnInterval);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return _wait;

            Transform point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Enemy enemy = _pool.Get();
            enemy.transform.position = point.position;
            G.EnemySystem.Register(enemy);
        }
    }
}
