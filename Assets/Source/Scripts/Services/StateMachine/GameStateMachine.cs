using System;
using System.Collections.Generic;

public class GameStateMachine : IDisposable
{
    private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
    private IGameState _current;
    private ILogger _logger;
    private IHealthService _healthService;

    public void Init(ILogger logger, IHealthService healthService)
    { 
        _logger = logger; 
        _healthService = healthService;
        _healthService.OnDied += GameOver;
    }

    public void Register<TState>(TState state) where TState : IGameState
    {
        _states[state.GetType()] = state;
    }

    public void ChangeState<TState>() where TState : IGameState
    {
        var type = typeof(TState);

        if (_states.TryGetValue(type, out IGameState next))
        {
            _current?.Exit();
            _current = next;
            _current.Enter();
            _logger.Log($"Game state changed, crrent state is {type}");
        }
    }

    public void Tick(float deltaTime)
    {
        if (_current is IUpdatableState updatable)
        {
            updatable.Tick(deltaTime); 
        }
    }

    public void Dispose()
    {
        _healthService.OnDied -= GameOver;
    }

    private void GameOver()
    {
        ChangeState<GameOverState>();
    }
}
