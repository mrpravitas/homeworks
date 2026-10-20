using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _initialPoolSize;

    private Pool<Projectile> _pool;
    private float _fireTimer;

    private void Awake()
    {
        _pool = new Pool<Projectile>(_projectilePrefab, _initialPoolSize);
        G.ProjectilePool = _pool;
        _fireTimer = 1f / _fireRate;
    }

    private void Update()
    {
        _fireTimer -= Time.deltaTime;

        if (_fireTimer > 0f || !Input.GetKey(KeyCode.Space))
        {
            return;
        }

        _fireTimer = 1f / _fireRate;
        Shoot();
    }

    private void Shoot()
    {
        Projectile projectile = _pool.Get();
        projectile.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
        projectile.SetSpeed(_projectileSpeed);
        G.ProjectileSystem.Register(projectile);
    }
}
