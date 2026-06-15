using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    [SerializeField] private int _selectedIndex;

    private void Awake()
    {
        ApplySelectedConfig();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _weapon.Shoot();
        }
    }

    private void ApplySelectedConfig()
    {
        if (_weapon == null || _weaponConfigs == null || _weaponConfigs.Count == 0)
            return;

        if (_selectedIndex < 0 || _selectedIndex >= _weaponConfigs.Count)
            _selectedIndex = 0;

        _weapon.SetConfig(_weaponConfigs[_selectedIndex]);
    }
}
