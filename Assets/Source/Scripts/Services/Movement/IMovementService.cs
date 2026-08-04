using UnityEngine;

public interface IMovementService
{
    float SpeedMultiplier { get; set; }
    void Move(Vector2 direction);
}
