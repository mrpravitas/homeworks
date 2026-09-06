using Mirror;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _grenadePickupPrefab;
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
            SpawnGrenade(_spawnPoints[i]);
        }
    }

    private void SpawnGrenade(Transform point)
    {
        GameObject gameObject = Instantiate(_grenadePickupPrefab, point.position, point.rotation);
        NetworkServer.Spawn(gameObject);
    }
}