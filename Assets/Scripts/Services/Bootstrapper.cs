using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private GameStateService _gameStateService;
    private SpawnService _spawnService;

    private void Awake()
    {
        _gameStateService = new GameStateService(_gameConfig);
        _gameStateService.Init();

        _spawnService = new SpawnService(_gameConfig);
        _spawnService.Init();
    }
}
