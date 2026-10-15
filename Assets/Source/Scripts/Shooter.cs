using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _projectileSpeed;

    private float _fireTimer;

    private void Awake()
    {
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
        Projectile projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        projectile.SetSpeed(_projectileSpeed);
    }
}
