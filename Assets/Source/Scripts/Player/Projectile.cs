using UnityEngine;

public class Projectile : MonoBehaviour, ITickable, IPoolable
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private float _speed;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnGet()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void OnReturn()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
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

        G.ProjectilePool.Release(this);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
