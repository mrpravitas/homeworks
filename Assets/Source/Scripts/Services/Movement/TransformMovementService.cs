using UnityEngine;

public class TransformMovementService : IMovementService
{
    private Transform _transform;
    private float _movementSpeed = 10f;

    public void Init(Transform transform)
    {
        _transform = transform;
    }

    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        Vector3 move = new Vector3(direction.x, direction.y, 0f);
        _transform.Translate(move * (_movementSpeed * Time.deltaTime));
    }
}
