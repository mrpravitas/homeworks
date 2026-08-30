using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _maxLookAngle;
    [SerializeField] private float _jumpForce;

    private Transform _transform;
    [SyncVar] private Vector3 _serverMoveDirection;
    [SyncVar] private float _serverRotationY;
    private float _cameraXRotation;
    private float _targetRotationY;
    private float _verticalVelocity;
    private bool _jumpPressed;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        if (isOwned)
        {
            _camera.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (!isOwned)
        { 
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = _transform.right * horizontal + _transform.forward * vertical;
        CmdSetMoveDirection(move);

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        _targetRotationY += mouseX;
        CmdSetRotation(_targetRotationY);

        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _cameraXRotation -= mouseY;
        _cameraXRotation = Mathf.Clamp(_cameraXRotation, -_maxLookAngle, _maxLookAngle);
        _camera.transform.localEulerAngles = new Vector3(_cameraXRotation, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            _jumpPressed = true;
            CmdJump();
        }
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    [Command]
    private void CmdSetMoveDirection(Vector3 direction)
    {
        _serverMoveDirection = direction.normalized;
    }

    [Command]
    private void CmdSetRotation(float rotationY)
    {
        _serverRotationY = rotationY;
    }

    private void FixedUpdate()
    {
        _transform.rotation = Quaternion.Euler(0, _serverRotationY, 0);

        if (_characterController.isGrounded && _verticalVelocity < 0)
        { 
            _verticalVelocity = -2f;
        }

        if (_jumpPressed && _characterController.isGrounded)
        {
            _verticalVelocity = _jumpForce;
        }

        _jumpPressed = false;
        _verticalVelocity += _gravity * Time.fixedDeltaTime;

        Vector3 velocity = _serverMoveDirection * _moveSpeed;
        velocity.y = _verticalVelocity;
        _characterController.Move(velocity * Time.fixedDeltaTime);
    }

    [Command]
    private void CmdJump()
    {
        RpcJump();
    }

    [ClientRpc]
    private void RpcJump()
    {
        if (isOwned) return; 
        if (_characterController.isGrounded)
            _verticalVelocity = _jumpForce;
    }
}
