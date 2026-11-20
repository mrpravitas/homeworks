using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private EntitySpawnEntry[] _spawnEntries;
    [SerializeField] private PlayerController _player;

    [Header("Modifiers")]
    [SerializeField] private bool _enableDoubleDamage;
    [SerializeField] private bool _enableHighSpeed;
    [SerializeField] private bool _enableHighSpawn;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameInstaller>();
        builder.RegisterComponentInHierarchy<SwitchInputButton>();
        builder.RegisterComponent(_player);

        builder.RegisterInstance(_movementConfig);
        builder.RegisterInstance(_healthConfig);
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
    }
}
