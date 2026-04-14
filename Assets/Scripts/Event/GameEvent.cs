public class GameEvent
{
    private EventType _eventType;
    private object _parameter;

    public EventType EventType => _eventType;
    public object Parameter => _parameter;

    public GameEvent(EventType eventType, object parameter = null)
    {
        _eventType = eventType;
        _parameter = parameter;
    }
}
