using System.Collections.Generic;
using System.Linq;

public static class EventManager
{
    public delegate void GameEventHandler(GameEvent gameEvent);

    public static event GameEventHandler OnGameEvent;

    private static List<GameEvent> _gameEvents = new();

    public static void TriggerEvent(GameEvent gameEvent)
    {
        _gameEvents.Add(gameEvent);
        OnGameEvent?.Invoke(gameEvent);
    }

    public static IEnumerable<GameEvent> GetAllEvents()
    {
        return _gameEvents;
    }

    public static IEnumerable<GameEvent> GetEventsByType(EventType eventType)
    {
        return _gameEvents.Where(e => e.EventType == eventType);
    }

    public static int CountEventsByType(EventType eventType)
    {
        return _gameEvents.Count(e => e.EventType == eventType);
    }

    public static IEnumerable<KeyValuePair<EventType, int>> GetMostFrequentEvents()
    {
        return _gameEvents
            .GroupBy(e => e.EventType)
            .OrderByDescending(e => e.Count())
            .Select(g => new KeyValuePair<EventType, int>(g.Key, g.Count()));
    }

    public static IEnumerable<GameEvent> GetLastEvents(int n)
    {
        return _gameEvents.OrderByDescending(e => e.EventTime).Take(n);
    }
}