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
        if (gameEvent.EventType != EventType.GameWon)
        {
            return;
        }

        Time.timeScale = 0f;
        Debug.Log("Game paused. You win!");
    }
}
