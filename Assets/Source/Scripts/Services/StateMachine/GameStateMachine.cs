using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
    private IGameState _current;

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
        }
    }
}
