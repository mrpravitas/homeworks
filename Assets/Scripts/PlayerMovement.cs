using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private Vector3 _direction;
    private Transform _transform;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        _direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude < 0.01f)
            return;

        Vector3 newPosition = _transform.position +
                              _direction * (_gameConfig.PlayerSpeed * Time.fixedDeltaTime);

        _rigidbody.MovePosition(newPosition);
    }
}
