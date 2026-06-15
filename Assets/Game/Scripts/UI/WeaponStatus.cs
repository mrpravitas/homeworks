using TMPro;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weaponStatus;

    private int _currentWeaponMagazineSize;
    private string _currentWeaponName;

    private void OnEnable()
    {
        Weapon.OnWeaponSwitched += HandleWeaponSwitched;
        Weapon.OnShot += HandleShot;
        Weapon.OnReloadStarted += HandleReloadStarted;
    }

    private void OnDisable()
    {
        Weapon.OnWeaponSwitched -= HandleWeaponSwitched;
        Weapon.OnShot -= HandleShot;
        Weapon.OnReloadStarted -= HandleReloadStarted;
    }

    private void HandleWeaponSwitched(string weaponName, int ammo, int maxAmmo)
    {
        _currentWeaponName = weaponName;
        _currentWeaponMagazineSize = maxAmmo;
        UpdateWeaponStatus(weaponName, ammo, maxAmmo);
    }

    private void HandleShot(int ammo)
    {
        UpdateWeaponStatus(_currentWeaponName, ammo, _currentWeaponMagazineSize);
    }

    private void HandleReloadStarted()
    {
        _weaponStatus.text = $"{_currentWeaponName} | reloading...";
    }

    private void UpdateWeaponStatus(string weaponName, int ammo, int maxAmmo)
    {
        _weaponStatus.text = $"{weaponName} | {ammo}/{maxAmmo}";
    }
}
