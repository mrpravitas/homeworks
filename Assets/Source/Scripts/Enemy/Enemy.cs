using System;
using UnityEngine;

public class Enemy : MonoBehaviour, ITickable, IPoolable
{
    public static event Action<int> OnDied;

    [SerializeField] private float _speed;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnGet()
    {
    }

    public void OnReturn()
    {
    }

    public void Tick(float deltaTime)
    {
        Vector3 position = _transform.position;
        position.z -= _speed * deltaTime;
        _transform.position = position;
    }

    public void Kill(bool fromPlayer = false)
    {
        OnDied?.Invoke(fromPlayer ? 1 : -1);
        G.EnemyPool.Release(this);
    }
}
