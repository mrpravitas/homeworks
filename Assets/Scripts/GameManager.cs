using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _distanceFromCamera;
    [Range(0f, 0.5f)]
    [SerializeField] private float _margin;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0, _spawnInterval);
    }

    private void SpawnTarget()
    {
        float randomX = Random.Range(_margin, 1f - _margin);
        float randomY = Random.Range(_margin, 1f - _margin);

        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, _distanceFromCamera));

        GameObject.Instantiate(_targetPrefab, position, Quaternion.identity);
    }
}
