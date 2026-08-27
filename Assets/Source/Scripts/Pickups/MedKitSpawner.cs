using Mirror;
using UnityEngine;

public class MedKitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _medKitPrefab;
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
            SpawnMedKit(_spawnPoints[i]);
        }
    }

    private void SpawnMedKit(Transform point)
    {
        GameObject gameObject = Instantiate(_medKitPrefab, point.position, point.rotation);
        NetworkServer.Spawn(gameObject);
    }
}
