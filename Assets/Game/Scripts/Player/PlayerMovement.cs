using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _input;

    private void Awake()
    {
        G.PlayerTransform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = 0f;
        _input.z = Input.GetAxisRaw("Vertical");
        _input.Normalize();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = _rigidbody.position + _input * (_movementSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}
