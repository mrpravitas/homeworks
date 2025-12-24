using UnityEngine;

[CreateAssetMenu(fileName = "cannonConfig", menuName = "Cannon/cannonConfig")]
public class CannonConfig : ScriptableObject
{
    [SerializeField] private float _shotForce;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _damage;

    public float ShotForce => _shotForce;
    public float ReloadTime => _reloadTime;
    public float Damage => _damage;
}
