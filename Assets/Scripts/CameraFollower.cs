using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Transform _camera;

    private void Awake()
    {
        _camera = transform;
    }

    private void LateUpdate()
    {
        _camera.position = new Vector3(_target.position.x, _camera.position.y, _target.position.z);
    }
}
