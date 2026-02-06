using System.Security.Cryptography;
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
        string log = "" +
            $"Event type: {gameEvent.EventType.ToString()}, " +
            $"description: \"{gameEvent.Description}\"";

        switch (gameEvent.EventType)
        {
            case EventType.EnemySpotted:
                Vector2 position = (Vector2)gameEvent.Parameter;
                log += $", enemy position: {position}";
                break;
            case EventType.EnemyDefeated:
                Vector2 deathPosition = (Vector2)gameEvent.Parameter;
                log += $", enemy death position: {deathPosition}";
                break;
            case EventType.EarthquakeStarted:
                float[] f = (float[])gameEvent.Parameter;
                float duration = f[0];
                float magnitude = f[1];
                log += $", duration: {duration}, strength: {magnitude}";
                break;
        }

        Debug.Log(log);
    }
}
