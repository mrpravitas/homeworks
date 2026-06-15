using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    
    private int _currentWeaponIndex = 0;

    private void Start()
    {
        ApplySelectedConfig();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _weapon.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchWeapon();
        }
    }

    private void ApplySelectedConfig()
    {
        _weapon.SetConfig(_weaponConfigs[_currentWeaponIndex]);

        Debug.Log($"Weapon switched to: {_weaponConfigs[_currentWeaponIndex].name}");
    }

    private void SwitchWeapon()
    {
        if (_weapon.IsReloading)
        {
            return;
        }

        _currentWeaponIndex++;

        if (_currentWeaponIndex >= _weaponConfigs.Count)
        {
            _currentWeaponIndex = 0;
        }

        ApplySelectedConfig();
    }
}
