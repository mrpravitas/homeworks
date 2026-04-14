using System;

public static class EventBus
{
    public static event Action<GameEvent> OnGameEvent;

    public static void Raise(GameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }
}
