using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<int> OnDied;

    [SerializeField] private float _speed;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Vector3 position = _transform.position;
        position.z -= _speed * Time.deltaTime;
        _transform.position = position;
    }

    public void Kill(bool fromPlayer = false)
    {
        OnDied?.Invoke(fromPlayer ? 1 : -1);
        Destroy(gameObject);
    }
}
