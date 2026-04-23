using System.Collections.Generic;
using UnityEngine;

public class EventLogService : IDisposable
{
    private List<GameEvent> _gameEvents;

    public EventLogService()
    {
        _gameEvents = new List<GameEvent>();
    }

    public void Init()
    {
        EventBus.OnGameEvent += HandleEvent;
    }

    public void Dispose()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }

    private void LogLastEvents(int count)
    {
        int start = Mathf.Max(0, _gameEvents.Count - count);
        List<GameEvent> last = _gameEvents.GetRange(start, _gameEvents.Count - start);

        for (int i = 0; i < last.Count; i++)
        {
            Debug.Log("Event: " + last[i].EventType + " | Parameter: " + last[i].Parameter);
        }
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        _gameEvents.Add(gameEvent);

        if (gameEvent.EventType == EventType.LogRequested)
        {
            LogLastEvents(5);
        }
    }
}
