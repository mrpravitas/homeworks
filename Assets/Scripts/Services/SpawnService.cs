using System.Threading.Tasks;

public class SpawnService
{
    private GameConfig _gameConfig;
    private bool _isRunning;
    private bool _isPaused;

    public SpawnService(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    public void Init()
    {
        _isRunning = false;
        _isPaused = false;
        EventBus.OnGameEvent += HandleEvent;
    }

    public void Dispose()
    {
        EventBus.OnGameEvent -= HandleEvent;
        _isRunning = false;
        _isPaused = false;
    }


    private void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType != EventType.GameStateChanged)
        {
            return;
        }

        GameState gameState = (GameState)gameEvent.Parameter;

        if (gameState == GameState.Playing)
        {
            _isPaused = false;

            if (!_isRunning)
            {
                _isRunning = true;
                RunSpawning();
            }

            return;
        }

        if (gameState == GameState.Paused)
        {
            _isPaused = true;
            return;
        }

        if (gameState == GameState.Win)
        {
            _isRunning = false;
            return;
        }

        if (gameState == GameState.Lose)
        {
            _isRunning = false;
            return;
        }
    }

    private async void RunSpawning()
    {
        while (_isRunning)
        {
            while (_isPaused)
            {
                await Task.Delay(100);
            }

            await Task.Delay((int)(_gameConfig.EnemySpawnInterval * 1000));

            if (!_isRunning)
            {
                break;
            }

            if (_isPaused)
            {
                continue;
            }

            EventBus.Raise(new GameEvent(EventType.EnemySpawned));
        }
    }
}
