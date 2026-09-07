using Mirror;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private Pickup _pickupPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _spawnCount;

    private void Start()
    {
        if (!NetworkServer.active)
        {
            return;
        }

        for (int i = 0; i < _spawnCount && i < _spawnPoints.Length; i++)
        {
            SpawnPickup(_spawnPoints[i]);
        }
    }

    private void SpawnPickup(Transform point)
    {
        GameObject gameObject = Instantiate(_pickupPrefab.gameObject, point.position, point.rotation);
        NetworkServer.Spawn(gameObject);
    }
}