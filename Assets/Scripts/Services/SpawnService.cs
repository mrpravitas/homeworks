using System.Threading.Tasks;

public class SpawnService
{
    private GameConfig _gameConfig;
    private bool _isRunning;

    public SpawnService(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    public void Init()
    {
        _isRunning = true;
        EventBus.OnGameEvent += HandleEvent;
        RunSpawning();
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == EventType.GameWon)
        {
            _isRunning = false;
        }
    }

    private async void RunSpawning()
    {
        while (_isRunning)
        {
            await Task.Delay((int)(_gameConfig.EnemySpawnInterval * 1000));

            if (!_isRunning)
            {
                break;
            }

            EventBus.Raise(new GameEvent(EventType.EnemySpawned));
        }
    }
}
