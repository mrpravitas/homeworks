using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private CharacterController _characterController;

    private Transform _transform;
    private Vector3 _serverMoveDirection;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        if (isOwned)
        {
            _camera.gameObject.SetActive(true);
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
    }

    [Command]
    private void CmdSetMoveDirection(Vector3 direction)
    {
        _serverMoveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _serverMoveDirection * _moveSpeed;
        velocity.y += _gravity;
        _characterController.Move(velocity * Time.fixedDeltaTime);
    }
}
