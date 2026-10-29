using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity;

    [Header("Look")]
    [SerializeField] private float _lookSensitivityX;
    [SerializeField] private float _lookSensitivityY;
    [SerializeField] private float _pitchLimit;

    private Vector3 _velocity;
    private Vector2 _moveInput;
    private float _pitch;

    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void Look(Vector2 delta)
    {
        if (delta.sqrMagnitude < 0.0001f)
        {
            return;
        }

        transform.Rotate(Vector3.up * delta.x * _lookSensitivityX);

        _pitch -= delta.y * _lookSensitivityY;
        _pitch = Mathf.Clamp(_pitch, -_pitchLimit, _pitchLimit);

        if (_playerCamera != null)
        {
            _playerCamera.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (_playerCamera != null)
        {
            _pitch = _playerCamera.transform.localEulerAngles.x;
            if (_pitch > 180f)
            {
                _pitch -= 360f;
            }
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float inputX = _moveInput.x;
        float inputZ = _moveInput.y;

        Vector3 wishDirection = transform.right * inputX + transform.forward * inputZ;
        if (wishDirection.sqrMagnitude > 1f)
        {
            wishDirection.Normalize();
        }

        Vector3 horizontalVelocity = wishDirection * _walkSpeed;

        if (_characterController.isGrounded)
        {
            if (_velocity.y < 0f)
            {
                _velocity.y = -2f;
            }
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move((horizontalVelocity + _velocity) * Time.deltaTime);
    }
}
