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
    private int _hitCount;
    private int _bestHitCount;

    public UnityEvent<int> OnTargetSpawned;
    public UnityEvent<int> OnHitTargetCountChanged;

    private void Start()
    {
        _bestHitCount = PlayerPrefs.GetInt("best hit count", 0);
        InvokeRepeating(nameof(SpawnTarget), 0, _spawnInterval);
    }

    private void SpawnTarget()
    {
        GameObject targetObject;

        if (_easyMode)
        {
            targetObject = Instantiate(GetRandomTarget(), Vector3.zero, Quaternion.identity);
            targetObject.GetComponent<Target>().OnHit += IncreaseHitCount;
            return;
        }

        float randomX = Random.Range(_margin, 1f - _margin);
        float randomY = Random.Range(_margin, 1f - _margin);

        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, _distanceFromCamera));

        targetObject = Instantiate(GetRandomTarget(), position, Quaternion.identity);
        targetObject.GetComponent<Target>().OnHit += IncreaseHitCount;

        _targetCount++;
        OnTargetSpawned.Invoke(_targetCount);

        Debug.Log($"{_targetCount} targets have been spawned");
    }

    private GameObject GetRandomTarget()
    {
        return _targetPrefabs[Random.Range(0, _targetPrefabs.Length)];
    }

    private void IncreaseHitCount()
    {
        _hitCount++;
        PlayerPrefs.SetInt("destroyed targets", PlayerPrefs.GetInt("destroyed targets", 0) + 1);

        if (_hitCount > _bestHitCount)
        {
            _bestHitCount = _hitCount;
            PlayerPrefs.SetInt("best hit count", _bestHitCount);
        }

        OnHitTargetCountChanged.Invoke(_hitCount);
    }
}
