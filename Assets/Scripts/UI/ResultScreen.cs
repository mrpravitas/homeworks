using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private void Start()
    {
        EventBus.OnGameEvent += OnGameEnded;
    }

    private void OnDestroy()
    {
        EventBus.OnGameEvent -= OnGameEnded;
    }

    private void OnDisable()
    {
        EventBus.OnGameEvent -= OnGameEnded;
    }

    private void OnGameEnded(GameEvent gameEvent)
    {
        if (gameEvent.EventType == EventType.GameStateChanged)
        {
            GameState gameState = (GameState)gameEvent.Parameter;

            if (gameState == GameState.Win)
            {
                _winScreen.SetActive(true);
            }
            else if (gameState == GameState.Lose)
            {
                _loseScreen.SetActive(true);
            }
        }
    }
}
