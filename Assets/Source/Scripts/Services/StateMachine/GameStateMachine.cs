using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
    private IGameState _current;
    private ILogger _logger;

    public void Init(ILogger logger)
    { 
        _logger = logger; 
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
}
