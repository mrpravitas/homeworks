using UnityEngine;

[CreateAssetMenu(fileName = "weapon config")]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private int _damage;
    [SerializeField] private int _magazineSize;
    [SerializeField] private float _reloadTime;

    public GameObject ProjectilePrefab => _projectilePrefab;
    public float ProjectileSpeed => _projectileSpeed;
    public float FireRate => _fireRate;
    public float SpreadAngle => _spreadAngle;
    public int Damage => _damage;
    public int MagazineSize => _magazineSize;
    public float ReloadTime => _reloadTime;
}
