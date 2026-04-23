using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private List<IDisposable> _services = new List<IDisposable>();

    private void Awake()
    {
        SpawnService spawnService = new SpawnService(_gameConfig);
        _services.Add(spawnService);
        spawnService.Init();

        GameStateService gameStateService = new GameStateService(_gameConfig);
        _services.Add(gameStateService);
        gameStateService.Init();

        EventLogService eventLogService = new EventLogService();
        _services.Add(eventLogService);
        eventLogService.Init();
    }

    private void OnDisable()
    {
        DisposeAllServices();
    }

    private void DisposeAllServices()
    {
        foreach (var service in _services)
        {
            service.Dispose();
        }
    }
}
