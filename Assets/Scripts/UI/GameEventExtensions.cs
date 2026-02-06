using UnityEngine;

public static class GameEventExtensions
{
    public static string ToLogString(this GameEvent gameEvent)
    {
        string log = $"Event type: {gameEvent.EventType}\n" +
            $"Description: \"{gameEvent.Description}\"";

        switch (gameEvent.EventType)
        {
            case EventType.EnemySpotted:
                Vector2 position = (Vector2)gameEvent.Parameter;
                log += $"\nEnemy position: {position}";
                break;
            case EventType.EnemyDefeated:
                Vector2 deathPosition = (Vector2)gameEvent.Parameter;
                log += $"\nEnemy death position: {deathPosition}";
                break;
            case EventType.EarthquakeStarted:
                float[] f = (float[])gameEvent.Parameter;
                float duration = f[0];
                float magnitude = f[1];
                log += $"\nDuration: {duration}, strength: {magnitude}";
                break;
        }

        return log + "\n";
    }
}
