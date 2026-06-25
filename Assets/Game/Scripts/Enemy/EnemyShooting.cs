using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileLifeTime;
    [SerializeField] private float _fireRate;

    private ProjectilePool _pool;
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

    public void SetPool(ProjectilePool pool)
    {
        _pool = pool;
    }

    private void Shoot()
    {
        GameObject projectileObject = _pool.Get();

        projectileObject.transform.position = _firePoint.position;

        Vector3 direction = (_target.position - _firePoint.position).normalized;
        projectileObject.transform.forward = direction;

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Initialize(_projectileLifeTime, _projectileSpeed);
    }
}
