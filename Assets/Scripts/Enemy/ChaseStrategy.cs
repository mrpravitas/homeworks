using UnityEngine;

public class ChaseStrategy : IEnemyStrategy
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Transform _target;
    private float _speed;

    public ChaseStrategy(Transform transform, Rigidbody rigidbody, Transform target, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _target = target;
        _speed = speed;
    }

    public void Tick(float deltaTime)
    {
        Vector3 direction = (_target.position - _transform.position).normalized;
        Vector3 newPosition = _transform.position + direction * (_speed * deltaTime);

        _rigidbody.MovePosition(newPosition);
    }
}
