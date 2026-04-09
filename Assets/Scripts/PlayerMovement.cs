using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private Vector3 _direction;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        _direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        _transform.position += _direction * (Time.deltaTime * _gameConfig.PlayerSpeed);
    }
}
