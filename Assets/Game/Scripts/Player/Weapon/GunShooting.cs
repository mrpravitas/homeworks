using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    
    private int _currentWeaponIndex = 0;
    private StringBuilder _stringBuilder;

    private void Awake()
    {
        _stringBuilder = new StringBuilder();
    }

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

        _stringBuilder.Clear();
        _stringBuilder.Append("Weapon switched to: ");
        _stringBuilder.Append(_weaponConfigs[_currentWeaponIndex].name);

        Debug.Log(_stringBuilder.ToString());
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
