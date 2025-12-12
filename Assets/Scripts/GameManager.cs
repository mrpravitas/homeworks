using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0, 4);
    }

    private void SpawnTarget()
    {
        GameObject.Instantiate(_targetPrefab);
    }
}
