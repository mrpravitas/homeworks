using UnityEngine;

public class Projectile : MonoBehaviour, ITickable
{
    private Transform _transform;
    private float _speed;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDestroy()
    {
        G.ProjectileSystem.Unregister(this);
    }

    public void Tick(float deltaTime)
    {
        _transform.position += _transform.forward * (_speed * deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Kill(true);
        }

        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
