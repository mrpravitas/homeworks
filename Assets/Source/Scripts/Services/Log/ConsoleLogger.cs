using System;
using UnityEngine;
using VContainer;

public sealed class ConsoleLogger : ILogger
{
    private IDisposable _gameStartedSubscription;
    private IDisposable _inputSwitchedSubscription;

    [Inject]
    public void Construct(IMessageBus bus)
    {
        DangerZone.OnEvent += Log;
        HealthPickup.OnEvent += Log;

        _gameStartedSubscription = bus.Subscribe<GameStartedSignal>(OnGameStarted);
        _inputSwitchedSubscription = bus.Subscribe<InputSwitchedSignal>(OnInputSwitched);
    }

    public void Dispose()
    {
        DangerZone.OnEvent -= Log;
        HealthPickup.OnEvent -= Log;

        _gameStartedSubscription?.Dispose();
        _inputSwitchedSubscription?.Dispose();
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }

    private void OnGameStarted(GameStartedSignal signal)
    {
        Log("Game has started");
    }

    private void OnInputSwitched(InputSwitchedSignal signal)
    {
        Log("Input method switched");
    }
}
