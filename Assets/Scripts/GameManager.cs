using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetPrefabs;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _distanceFromCamera;
    [Range(0f, 0.5f)]
    [SerializeField] private float _margin;
    [Header("For testing")]
    [SerializeField] private bool _easyMode;

    private int _targetCount;

    public UnityEvent<int> OnTargetSpawned;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0, _spawnInterval);
    }

    private void SpawnTarget()
    {
        if (_easyMode)
        {
            Instantiate(GetRandomTarget(), Vector3.zero, Quaternion.identity);
            return;
        }

        float randomX = Random.Range(_margin, 1f - _margin);
        float randomY = Random.Range(_margin, 1f - _margin);

        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, _distanceFromCamera));

        Instantiate(GetRandomTarget(), position, Quaternion.identity);

        _targetCount++;
        OnTargetSpawned.Invoke(_targetCount);

        Debug.Log($"{_targetCount} targets have been spawned");
    }

    private GameObject GetRandomTarget()
    {
        return _targetPrefabs[Random.Range(0, _targetPrefabs.Length)];
    }
}
