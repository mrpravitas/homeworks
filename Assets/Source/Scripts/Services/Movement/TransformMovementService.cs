using UnityEngine;

public class TransformMovementService : IMovementService
{
    private MovementConfig _movementConfig;

    private Transform _transform;

    public void Init(MovementConfig movementConfig, Transform transform)
    {
        _transform = transform;
        _movementConfig = movementConfig;
    }

    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        Vector3 move = new Vector3(direction.x, direction.y, 0f);
        _transform.Translate(move * (_movementConfig.MovementSpeed * Time.deltaTime));
    }
}
