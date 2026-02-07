using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _showAnalyticsButton;
    [SerializeField] private Button _allLogsButton;
    [SerializeField] private Button _spawnedEnemiesLogsButton;
    [SerializeField] private Button _defeatedEnemiesLogsButton;
    [SerializeField] private Button _weatherLogsButton;
    [SerializeField] private Button _earthquakeLogsButton;

    [Header("Analytics")]
    [SerializeField] private TMP_Text _lastEventsText;
    [SerializeField] private TMP_Text _frequentEventsText;
    [SerializeField] private TMP_Text _spawnedEnemyText;
    [SerializeField] protected TMP_Text _defeatdEnemyText;

    [Header("Logs")]
    [SerializeField] private Transform _viewportContent;
    [SerializeField] private TMP_Text _logPrefab;

    private LogFilter _currentFilter = LogFilter.All;

    private void OnEnable()
    {
        EventManager.OnGameEvent += LogEvent;
        _showAnalyticsButton.onClick.AddListener(ShowAnalytics);
        _allLogsButton.onClick.AddListener(ShowAllLogs);
        _spawnedEnemiesLogsButton.onClick.AddListener(ShowSpawnedEnemyLogs);
        _defeatedEnemiesLogsButton.onClick.AddListener(ShowDefeatedEnemyLogs);
        _weatherLogsButton.onClick.AddListener(ShowWeatherLogs);
        _earthquakeLogsButton.onClick.AddListener(ShowEarthquakeLogs);

    }

    private void OnDisable()
    {
        EventManager.OnGameEvent -= LogEvent;
        _showAnalyticsButton.onClick.RemoveListener(ShowAnalytics);
    }

    private void LogEvent(GameEvent gameEvent)
    {
        if (!IsPassFilter(gameEvent))
        {
            return;
        }

        TMP_Text log = Instantiate(_logPrefab, _viewportContent);
        log.text = gameEvent.ToLogString();
    }

    public void ShowAnalytics()
    {
        ShowLastEvents();
        ShowFrequentEvents();
        ShowEnemyStats();
    }

    private void ShowLastEvents()
    {
        IEnumerable<GameEvent> events = EventManager.GetLastEvents(5);

        _lastEventsText.text = "";
        foreach (GameEvent e in events)
        {
            _lastEventsText.text += e.ToLogString() + "\n";
        }
    }

    private void ShowFrequentEvents()
    {
        IEnumerable<KeyValuePair<EventType, int>> events = EventManager.GetMostFrequentEvents();

        _frequentEventsText.text = "";
        foreach (KeyValuePair<EventType, int> pair in events)
        {
            _frequentEventsText.text += $"{pair.Key}: {pair.Value} times; ";
        }
    }

    private void ShowEnemyStats()
    {
        int spawned = EventManager.CountEventsByType(EventType.EnemySpotted); 
        int defeated = EventManager.CountEventsByType(EventType.EnemyDefeated);

        _spawnedEnemyText.text = $"Enemies spawned: {spawned}";
        _defeatdEnemyText.text = $"Enemies defeated: {defeated}";
    }

    private void ClearUILogs()
    {
        foreach (Transform child in _viewportContent)
        {
            Destroy(child.gameObject);
        }
    }

    private void RenderLogs(IEnumerable<GameEvent> events)
    {
        ClearUILogs();

        foreach (GameEvent e in events)
        {
            TMP_Text log = Instantiate(_logPrefab, _viewportContent);
            log.text = e.ToLogString();
        }
    }

    private void ShowAllLogs()
    {
        _currentFilter = LogFilter.All;
        RenderLogs(EventManager.GetAllEvents());
    }

    private void ShowSpawnedEnemyLogs()
    {
        _currentFilter = LogFilter.EnemySpawned;
        RenderLogs(EventManager.GetEventsByType(EventType.EnemySpotted));
    }

    private void ShowDefeatedEnemyLogs()
    {
        _currentFilter = LogFilter.EnemyDefeated;
        RenderLogs(EventManager.GetEventsByType(EventType.EnemyDefeated));
    }

    private void ShowWeatherLogs()
    {
        _currentFilter = LogFilter.Weather;
        RenderLogs(EventManager.GetEventsByType(EventType.WeatherChanged));
    }

    private void ShowEarthquakeLogs()
    {
        _currentFilter = LogFilter.Earthquake;
        RenderLogs(EventManager.GetEventsByType(EventType.EarthquakeStarted));
    }

    private bool IsPassFilter(GameEvent gameEvent)
    {
        switch (_currentFilter)
        {
            case LogFilter.All:
                return true;
            case LogFilter.EnemySpawned:
                return gameEvent.EventType == EventType.EnemySpotted;
            case LogFilter.EnemyDefeated:
                return gameEvent.EventType == EventType.EnemyDefeated;
            case LogFilter.Weather:
                return gameEvent.EventType == EventType.WeatherChanged;
            case LogFilter.Earthquake:
                return gameEvent.EventType == EventType.EarthquakeStarted;
            default:
                return true;
        }
    }

    private enum LogFilter
    {
        All, 
        EnemySpawned, 
        EnemyDefeated,
        Weather, 
        Earthquake
    }
}