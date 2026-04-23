public class GameStateService : IDisposable
{
    private GameConfig _gameConfig;
    private int _score;
    private GameState _gameState;

    public GameStateService(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        EventBus.OnGameEvent += HandleEvent;
    }

    public void Init()
    {
        _score = 0;
        SetState(GameState.Init);
    }

    public void Dispose()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }


    private void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == EventType.ItemPicked)
        {
            _score++;
            EventBus.Raise(new GameEvent(EventType.ScoreChanged, _score));
            SetState(GameState.Playing);

            if (_score >= _gameConfig.TargetScore)
            {
                SetState(GameState.Win);
            }
        }

        if (gameEvent.EventType == EventType.GamePaused)
        {
            if (_gameState == GameState.Playing || _gameState == GameState.Init)
            {
                SetState(GameState.Paused);
                return;
            }

            if (_gameState == GameState.Paused)
            {
                SetState(GameState.Playing);
                return;
            }
        }

        if (gameEvent.EventType == EventType.PlayerDamaged)
        {
            SetState(GameState.Lose);
        }
    }

    private void SetState(GameState gameState)
    {
        if (_gameState == gameState)
        {
            return;
        }

        _gameState = gameState;
        EventBus.Raise(new GameEvent(EventType.GameStateChanged, _gameState));
    }
}
