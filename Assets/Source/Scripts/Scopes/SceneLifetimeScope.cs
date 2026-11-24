using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private GameplayConfig _gameplayConfig;
    [SerializeField] private EntitySpawnEntry[] _spawnEntries;
    [SerializeField] private PlayerController _player;

    [Header("Modifiers")]
    [SerializeField] private bool _enableDoubleDamage;
    [SerializeField] private bool _enableHighSpeed;
    [SerializeField] private bool _enableHighSpawn;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<SwitchInputButton>();
        builder.RegisterComponent(_player);

        builder.RegisterComponentInHierarchy<MainMenuView>().As<IMainMenuView>();
        builder.RegisterComponentInHierarchy<GameplayInputView>().As<IGameplayInput>();
        builder.RegisterComponentInHierarchy<GameplayHud>().As<IGameplayHud>();
        builder.RegisterComponentInHierarchy<PauseView>().As<IPauseView>();
        builder.RegisterComponentInHierarchy<GameOverView>().As<IGameOverView>();

        builder.RegisterInstance(_movementConfig);
        builder.RegisterInstance(_healthConfig);
        builder.RegisterInstance(_gameplayConfig);
        builder.RegisterInstance(_player.transform);

        builder.Register<IMovementService, TransformMovementService>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<UITextHealthPresenter>().As<IHealthPresenter>();
        builder.Register<IHealthService, HealthService>(Lifetime.Scoped);

        foreach (var entry in _spawnEntries)
        {
            var local = entry;
            builder.Register<IEntityFactory>(
                resolver => new EntityFactory(resolver, local.Prefab, local.Config, local.SpawnChance),
                Lifetime.Scoped);
        }

        if (_enableDoubleDamage)
        {
            builder.Register<IGameModifier, DoubleDamageModifier>(Lifetime.Scoped);
        }
        if (_enableHighSpeed)
        {
            builder.Register<IGameModifier, HighSpeedModifier>(Lifetime.Scoped);
        }
        if (_enableHighSpawn)
        {
            builder.Register<IGameModifier, HighSpawnMultiplier>(Lifetime.Scoped);
        }

        builder.RegisterEntryPoint<GameStateMachine>(Lifetime.Scoped);

        builder.Register<IGameState, MainMenuState>(Lifetime.Scoped);
        builder.Register<IGameState, GameplayState>(Lifetime.Scoped);
        builder.Register<IGameState, PauseState>(Lifetime.Scoped);
        builder.Register<IGameState, GameOverState>(Lifetime.Scoped);
    }
}
