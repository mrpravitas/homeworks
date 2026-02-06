using UnityEngine;

public class GodPanel : MonoBehaviour
{
    [Header("Weather")]
    [SerializeField] private GameObject _weatherPanel;

    [Header("Enemy")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Earthquake")]
    [SerializeField] private CameraShaker _cameraShaker;
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;

    public void ChangeWeather()
    {
        _weatherPanel?.SetActive(!_weatherPanel.activeSelf);

        GameEvent gameEvent = CreateGameEvent(
            EventType.WeatherChanged,
            Time.time,
            "weather has changed");

        EventManager.TriggerEvent(gameEvent);
    }

    public void SpawnEnemy()
    {
        Vector2 spawnPosition = Random.insideUnitCircle * 4;
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

        GameEvent gameEvent = CreateGameEvent(
            EventType.EnemySpotted,
            Time.time,
            "enemy has spawned. cick on the enemy to defeat",
            spawnPosition);

        EventManager.TriggerEvent(gameEvent);
    }

    public void StartEarthquake()
    {
        _cameraShaker?.Shake(_duration, _magnitude);

        float[] parameters = new float[] { _duration, _magnitude };
        GameEvent gameEvent = CreateGameEvent(
            EventType.EarthquakeStarted,
            Time.time,
            $"earthquake has started. duration: {_duration}, magnitude: {_magnitude}",
            parameters);
    }

    private GameEvent CreateGameEvent(EventType eventType, float time, string description, object parameter = null)
    {
        return new GameEvent(eventType, time, description, parameter);
    }
}
