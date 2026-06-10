using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _fireRate;

    private float _shootTimer;

    private void Update()
    {
        _shootTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && _shootTimer <= 0f)
        {
            Shoot();
            _shootTimer = 1f / _fireRate;
        } 
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.SetSpeed(_projectileSpeed);
    }
}
