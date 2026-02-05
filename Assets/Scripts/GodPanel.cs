using UnityEngine;
using UnityEngine.UI;

public class GodPanel : MonoBehaviour
{
    [Header("Weather")]
    [SerializeField] private GameObject _weatherPanel;

    [Header("Enemy")]
    [SerializeField] private GameObject _enemyPrefab;

    public void ChangeWeather()
    {
        _weatherPanel.SetActive(!_weatherPanel.activeSelf);
    }

    public void SpawnEnemy()
    {
        Vector2 spawnPosition = Random.insideUnitCircle * 4;
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void StartEarthquake()
    {

    }
}
