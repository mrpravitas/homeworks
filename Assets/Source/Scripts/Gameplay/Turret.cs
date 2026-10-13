using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _projectileSpeed;

    private Transform _transform;
    private float _fireTimer;

    private void Awake()
    {
        _transform = transform;
        _fireTimer = 1f / _fireRate;
    }

    private void Update()
    {
        _fireTimer -= Time.deltaTime;

        if (_fireTimer > 0f)
        {
            return;
        }

        _fireTimer += (1f / _fireRate);
        FireAtRandomTarget();
    }

    private void FireAtRandomTarget()
    {
        Transform target = _targets[Random.Range(0, _targets.Length)];
        Vector3 direction = (target.position - _transform.position).normalized;

        Projectile projectile = Instantiate(_projectilePrefab, _transform.position, _transform.rotation);
        projectile.Launch(direction, _projectileSpeed);
    }
}