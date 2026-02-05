using UnityEngine;

public class GameEvent
{
    private EventType _eventType;
    private float _eventTime;
    private string _description;
    private object _parameter;

    public GameEvent(EventType eventType, float eventTime, string description, object parameter = null)
    {
        _eventType = eventType;
        _eventTime = eventTime;
        _description = description;
        _parameter = parameter;
    }

    public EventType EventType => _eventType;
    public float EventTime => _eventTime;
    public string Description => _description;
    public object Parameter => _parameter;
}
