using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _enemyConfig;

    private Transform _target;
    private Transform _transform;
    private Rigidbody _rigidbody;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = (_target.position - _transform.position).normalized;
        Vector3 newPosition = _transform.position + direction * (_enemyConfig.Speed * Time.fixedDeltaTime);

        _rigidbody.MovePosition(newPosition);
    }
}
