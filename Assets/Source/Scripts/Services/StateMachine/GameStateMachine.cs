using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public sealed class GameStateMachine : IInitializable, ITickable, IDisposable
{
    private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
    private readonly IReadOnlyList<IGameState> _initialStates;
    private readonly ILogger _logger;
    private readonly IHealthService _healthService;
    private readonly IMessageBus _bus;

    private IDisposable _changeStateSubscription;
    private IGameState _current;

    public GameStateMachine(
        IReadOnlyList<IGameState> states,
        ILogger logger,
        IHealthService healthService,
        IMessageBus bus)
    {
        _initialStates = states;
        _logger = logger;
        _healthService = healthService;
        _bus = bus;
    }

    public void Initialize()
    {
        for (int i = 0; i < _initialStates.Count; i++)
        {
            Register(_initialStates[i]);
        }

        _changeStateSubscription = _bus.Subscribe<ChangeStateSignal>(OnChangeStateRequested);
        _healthService.OnDied += GameOver;

        _bus.Publish(new GameStartedSignal());
        ChangeState<MainMenuState>();
    }

    public void ChangeState<TState>() where TState : IGameState
    {
        ChangeState(typeof(TState));
    }

    public void Tick()
    {
        if (_current is IUpdatableState updatable)
        {
            updatable.Tick(Time.deltaTime);
        }
    }

    public void Dispose()
    {
        _changeStateSubscription.Dispose();
        _healthService.OnDied -= GameOver;
    }

    private void Register<TState>(TState state) where TState : IGameState
    {
        _states[state.GetType()] = state;
    }

    private void ChangeState(Type type)
    {
        if (_states.TryGetValue(type, out IGameState next))
        {
            _current?.Exit();
            _current = next;
            _current.Enter();
            _logger.Log($"Game state changed, crrent state is {type}");
        }
    }

    private void OnChangeStateRequested(ChangeStateSignal signal)
    {
        ChangeState(signal.StateType);
    }

    private void GameOver()
    {
        ChangeState<GameOverState>();
    }
}
