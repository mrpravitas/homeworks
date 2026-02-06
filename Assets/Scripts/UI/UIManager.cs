using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Analytics")]
    [SerializeField] private TMP_Text _lastEventsText;
    [SerializeField] private TMP_Text _frequentEventsText;
    [SerializeField] private TMP_Text _spawnedEnemyText;
    [SerializeField] protected TMP_Text _defeatdEnemyText;

    [Header("Logs")]
    [SerializeField] private Transform _viewportContent;
    [SerializeField] private TMP_Text _logPrefab;

    private void OnEnable()
    {
        EventManager.OnGameEvent += LogEvent;
    }

    private void OnDisable()
    {
        EventManager.OnGameEvent -= LogEvent;
    }

    private void LogEvent(GameEvent gameEvent)
    {
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
}
