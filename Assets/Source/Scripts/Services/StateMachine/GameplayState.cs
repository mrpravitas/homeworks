using UnityEngine;

public class GameplayState : IUpdatableState
{
    private GameStateMachine _stateMashine;
    private IGameplayInput _gameplayInput;
    private GameplayConfig _config;

    private IEntityFactory<Object>[] _factories;

    private float _timer;

    public GameplayState(GameStateMachine stateMashine, IGameplayInput gameplayInput, 
        GameplayConfig config, IEntityFactory<Object>[] factories)
    {
        _stateMashine = stateMashine;
        _gameplayInput = gameplayInput;
        _config = config;
        _factories = factories;
    }

    public void Enter()
    {
        Time.timeScale = 1f;
        _gameplayInput.SetPauseHandler(OnPause);
    }

    public void Exit()
    {
        _gameplayInput.SetPauseHandler(null);
    }

    public void Tick(float deltaTime)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        _timer += deltaTime;

        if (_timer >= _config.EntitySpawnIterval)
        {
            _timer = 0f;

            Vector3 position = GetRandomPosition();

            for (int i = 0; i < _factories.Length; i++)
            {
                _factories[i].Create(position);
            }
        }
    }

    private void OnPause()
    {
        _stateMashine.ChangeState<PauseState>();
    }

    private Vector3 GetRandomPosition()
    {
        float radius = _config.EntitySpawnRadius;
        float x = Random.Range(-radius, radius); 
        float z = Random.Range(-radius, radius);
        return new Vector3(x, 0f, z);
    }
}
