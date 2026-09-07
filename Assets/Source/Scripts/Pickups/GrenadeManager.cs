using Mirror;
using UnityEngine;

public class GrenadeManager : ItemManager
{
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _throwSpeed;

    protected override void Use()
    {
        Vector3 origin = _camera.transform.position;
        Vector3 direction = _camera.transform.forward;

        Grenade grenade = Instantiate(_grenadePrefab, origin, Quaternion.identity);
        NetworkServer.Spawn(grenade.gameObject);
        grenade.Launch(direction * _throwSpeed, netId);

        string who = GetComponent<GamePlayer>().Nickname;
        Debug.Log($"{who} threw a grenade");
    }
}