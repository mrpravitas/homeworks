using UnityEngine;

public class GamePause : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType != EventType.GameStateChanged)
        {
            return;
        }

        GameState gameState = (GameState)gameEvent.Parameter;

        if (gameState == GameState.Win)
        {
            Time.timeScale = 0f;
            return;
        }

        if (gameState == GameState.Lose)
        {
            Time.timeScale = 0f;
            return;
        }

        if (gameState == GameState.Paused)
        {
            Time.timeScale = 0f;
            return;
        }

        if (gameState == GameState.Playing)
        {
            Time.timeScale = 1f;
            return;
        }
    }
}
