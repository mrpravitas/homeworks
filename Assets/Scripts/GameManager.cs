using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetPrefabs;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _distanceFromCamera;
    [Range(0f, 0.5f)]
    [SerializeField] private float _margin;

    private int _targetCount;

    public UnityEvent<int> OnTargetSpawned;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0, _spawnInterval);
    }

    private void SpawnTarget()
    {
        float randomX = Random.Range(_margin, 1f - _margin);
        float randomY = Random.Range(_margin, 1f - _margin);

        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, _distanceFromCamera));

        GameObject target = _targetPrefabs[Random.Range(0, _targetPrefabs.Length)];

        GameObject.Instantiate(target, position, Quaternion.identity);

        _targetCount++;
        OnTargetSpawned.Invoke(_targetCount);

        Debug.Log($"{_targetCount} targets have been spawned");
    }
}
