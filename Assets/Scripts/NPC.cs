using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("OnWeatherChanged")]
    [SerializeField] private GameObject _umbrella;

    private void OnEnable()
    {
        EventManager.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventManager.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        switch (gameEvent.EventType)
        {
            case EventType.WeatherChanged:
                OnWeatherChanged();
                break;
            case EventType.EnemySpotted:
                OnEnemySpotted(gameEvent.Parameter);
                break;
            case EventType.EnemyDefeated:
                OnEnemyDefeated();
                break;
            case EventType.EarthquakeStarted:
                OnEarthquakeStarted(gameEvent.Parameter);
                break;
        }
    }

    private void OnWeatherChanged()
    {
        _umbrella.SetActive(!_umbrella.activeSelf);
    }

    private void OnEnemySpotted(object enemyPosition)
    {

    }

    private void OnEnemyDefeated()
    {

    }

    private void OnEarthquakeStarted(object duration)
    {

    }
}
