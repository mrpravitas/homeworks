using UnityEngine;

public class KeyboardInputService : IInputService
{
    public Vector2 GetMovementDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        return new Vector2(x, y).normalized;
    }
}
