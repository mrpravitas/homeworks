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
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _pitchLimit;

    private Vector3 _velocity;
    private float _pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, -_pitchLimit, _pitchLimit);

        _playerCamera.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    private void HandleMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

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

            if (Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move((horizontalVelocity + _velocity) * Time.deltaTime);
    }
}
