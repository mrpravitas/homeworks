using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        Vector3 position = _transform.position;
        position.x += direction * _speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, _minX, _maxX);
        _transform.position = position;
    }
}
