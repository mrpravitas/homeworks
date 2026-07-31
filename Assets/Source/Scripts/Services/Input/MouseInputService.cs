using UnityEngine;

public class MouseInputService : IInputService
{
    private float _centerX = Screen.width / 2;
    private float _centerY = Screen.height / 2;

    public Vector2 GetMovementDirection()
    {
        if (!Input.GetMouseButton(0))
        {
            return Vector2.zero;
        }

        Vector3 mouse = Input.mousePosition;

        float xDelta = mouse.x - _centerX;
        float yDelta = mouse.y - _centerY;

        return new Vector2(xDelta, yDelta).normalized;
    }
}
