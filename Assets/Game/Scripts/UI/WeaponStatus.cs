using System.Text;
using TMPro;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weaponStatus;

    private int _currentWeaponMagazineSize;
    private string _currentWeaponName;
    private StringBuilder _stringBuilder;

    private void Awake()
    {
        _stringBuilder = new StringBuilder();
    }

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
        _stringBuilder.Clear();
        _stringBuilder.Append(_currentWeaponName);
        _stringBuilder.Append(" | reloading...");

        _weaponStatus.text = _stringBuilder.ToString();
    }

    private void UpdateWeaponStatus(string weaponName, int ammo, int maxAmmo)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(weaponName);
        _stringBuilder.Append(" | ");
        _stringBuilder.Append(ammo);
        _stringBuilder.Append("/");
        _stringBuilder.Append(maxAmmo);

        _weaponStatus.text = _stringBuilder.ToString();
    }
}
