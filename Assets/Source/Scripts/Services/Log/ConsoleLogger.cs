using System;
using UnityEngine;
using VContainer;

public sealed class ConsoleLogger : ILogger
{
    private IDisposable[] _subscriptions;

    [Inject]
    public void Construct(IMessageBus bus)
    {
        _subscriptions = new[]
        {
            bus.Subscribe<GameStartedSignal>(OnGameStarted),
            bus.Subscribe<InputSwitchedSignal>(OnInputSwitched),
            bus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged),
            bus.Subscribe<HealthPickupCollectedSignal>(OnHealthPickupCollected),
            bus.Subscribe<ZoneEventSignal>(OnZoneEvent),
        };
    }

    public void Dispose()
    {
        for (int i = 0; i < _subscriptions.Length; i++)
        {
            _subscriptions[i].Dispose();
        }
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

    private void OnPlayerDamaged(PlayerDamagedSignal signal)
    {
        Log($"You took damage: {signal.Reason}");
    }

    private void OnHealthPickupCollected(HealthPickupCollectedSignal signal)
    {
        Log("Heal pickup collected");
    }

    private void OnZoneEvent(ZoneEventSignal signal)
    {
        Log(signal.Message);
    }
}
