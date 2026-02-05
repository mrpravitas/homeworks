using UnityEngine;

public class GodPanel : MonoBehaviour
{
    [Header("Weather")]
    [SerializeField] private GameObject _weatherPanel;

    [Header("Enemy")]
    [SerializeField] private GameObject _enemyPrefab;

    public void ChangeWeather()
    {
        _weatherPanel.SetActive(!_weatherPanel.activeSelf);

        GameEvent gameEvent = new GameEvent(
            EventType.WeatherChanged,
            Time.time,
            "weather has changed");

        EventManager.TriggerEvent(gameEvent);
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
