using UnityEngine;

public class UIEventLogger : MonoBehaviour
{
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
        Debug.Log(gameEvent.ToLogString());
    }
}
