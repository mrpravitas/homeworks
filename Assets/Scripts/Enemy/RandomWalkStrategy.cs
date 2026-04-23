using UnityEngine;

public class RandomWalkStrategy : IEnemyStrategy
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private float _speed;
    private State _state;
    private float _timer;
    private Vector3 _direction;

    public RandomWalkStrategy(Transform transform, Rigidbody rigidbody, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _speed = speed;

        _state = State.Waiting;
        _timer = 0f;
    }

    public void Tick(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= 1f)
        {
            if (_state == State.Waiting)
            {
                Vector2 random = Random.insideUnitCircle.normalized;
                _direction = new Vector3(random.x, 0f, random.y);

                _state = State.Moving;
            }
            else
            {
                _state = State.Waiting;
            }

            _timer = 0f;
        }

        if (_state == State.Waiting)
        {
            return;
        }

        Vector3 newPosition = _transform.position + _direction * (_speed * deltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private enum State
    {
        Waiting,
        Moving
    }
}
