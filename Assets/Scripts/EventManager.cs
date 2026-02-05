using System.Collections.Generic;

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
}