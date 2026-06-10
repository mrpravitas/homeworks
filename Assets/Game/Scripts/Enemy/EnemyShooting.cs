using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _fireRate;

    private Transform _target;
    private float _shootTimer;

    private void Start()
    {
        _target = G.PlayerTransform;
    }

    private void Update()
    {
        _shootTimer -= Time.deltaTime;

        if (_shootTimer <= 0f)
        {
            Shoot();
            _shootTimer = 1f / _fireRate;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

        Vector3 direction = (_target.position - _firePoint.position).normalized;
        projectile.transform.forward = direction;

        projectile.GetComponent<Projectile>().SetSpeed(_projectileSpeed);
    }
}
