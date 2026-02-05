using UnityEngine;

public class GameEvent
{
    private EventType _eventType;
    private float _eventTime;
    private string _description;

    public GameEvent(EventType eventType, float eventTime, string description)
    {
        _eventType = eventType;
        _eventTime = eventTime;
        _description = description;
    }

    public EventType EventType => _eventType;
    public float EventTime => _eventTime;
    public string Description => _description;
}
