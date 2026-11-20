using UnityEngine;

public class TransformMovementService : IMovementService
{
    private readonly MovementConfig _movementConfig;
    private readonly Transform _transform;

    public float SpeedMultiplier { get; set; } = 1f;

    public TransformMovementService(MovementConfig movementConfig, Transform transform)
    {
        _movementConfig = movementConfig;
        _transform = transform;
    }

    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        Vector3 move = new Vector3(direction.x, direction.y, 0f);
        _transform.Translate(move * (_movementConfig.MovementSpeed * SpeedMultiplier * Time.deltaTime));
    }
}
