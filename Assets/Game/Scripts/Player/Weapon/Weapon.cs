using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;

    private WeaponConfig _weaponConfig;
    private float _cooldown;
    private int _ammoInMagazine;
    private bool _isReloading = false;
    private Coroutine _reloadingCoroutine;

    private bool CanShoot => _cooldown <= 0f && !_isReloading && _ammoInMagazine > 0;

    private void Start()
    {
        _ammoInMagazine = _weaponConfig.MagazineSize;
    }

    private void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (!CanShoot)
        {
            return;
        }

        _ammoInMagazine--; 
        _cooldown = 1 / _weaponConfig.FireRate;

        if (_ammoInMagazine <= 0)
        {
            StartReload();
        }

        Quaternion baseDirection = _firePoint.rotation;
        Quaternion spreadDirection = baseDirection; 

        if (_weaponConfig.SpreadAngle > 0f)
        {
            float halfSpread = _weaponConfig.SpreadAngle / 2f;

            float randomY = Random.Range(-halfSpread, halfSpread);

            spreadDirection = baseDirection * Quaternion.Euler(0f, randomY, 0f);
        }

        GameObject projectileObject = Instantiate(_weaponConfig.ProjectilePrefab, _firePoint.position, spreadDirection);
        
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.SetSpeed(_weaponConfig.ProjectileSpeed);
        projectile.SetDamage(_weaponConfig.Damage);
    }

    public void SetConfig(WeaponConfig config)
    {
        _weaponConfig = config;
        _ammoInMagazine = _weaponConfig.MagazineSize;
        _isReloading = false;
        _cooldown = 0f;
    }

    private void StartReload()
    {
        if (_isReloading)
        {
            return;
        }

        _reloadingCoroutine = StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        _isReloading = true;
        Debug.Log("Reloading started");

        yield return new WaitForSeconds(_weaponConfig.ReloadTime);

        _ammoInMagazine = _weaponConfig.MagazineSize;
        _isReloading = false;
        Debug.Log("Reloading finished");
    }
}
