using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private Camera _camera;
    private Transform _transform;

    private static readonly Plane _plane = new Plane(Vector3.up, Vector3.zero);

    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    private void Update()
    {
        Rotate();
    } 

    private void Rotate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_plane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 direction = point - _transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                _transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
