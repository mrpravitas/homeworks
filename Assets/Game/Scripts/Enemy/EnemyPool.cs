using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private GameObject _prefab;
    private Queue<GameObject> _pool;

    public EnemyPool(GameObject prefab, int initialSize)
    {
        _prefab = prefab;
        _pool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject enemy = Object.Instantiate(_prefab);
            enemy.SetActive(false);

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.SetPool(this);

            _pool.Enqueue(enemy);
        }
    }

    public GameObject Get()
    {
        if (_pool.Count > 0)
        {
            GameObject enemy = _pool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }

        GameObject newEnemy = Object.Instantiate(_prefab);

        Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
        enemyComponent.SetPool(this);

        return newEnemy;
    }

    public void Return(GameObject enemy)
    {
        _pool.Enqueue(enemy);
    }
}
