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
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = _rigidbody.position + _input * (_movementSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}
