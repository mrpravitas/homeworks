using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool
{
    private GameObject _prefab;
    private Queue<GameObject> _pool;

    public ProjectilePool(GameObject prefab, int initialSize)
    {
        _prefab = prefab;
        _pool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject gameObject = Object.Instantiate(_prefab);
            gameObject.SetActive(false);

            Projectile projectile = gameObject.GetComponent<Projectile>();
            projectile.SetPool(this);

            _pool.Enqueue(gameObject);
        }
    }

    public GameObject Get()
    {
        if (_pool.Count > 0)
        {
            GameObject gameObject = _pool.Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }

        GameObject newObject = Object.Instantiate(_prefab);

        Projectile projectile = newObject.GetComponent<Projectile>();
        projectile.SetPool(this);

        return newObject;
    }

    public void Return(GameObject gameObject)
    {
        gameObject.SetActive(false);
        _pool.Enqueue(gameObject);
    }
}
