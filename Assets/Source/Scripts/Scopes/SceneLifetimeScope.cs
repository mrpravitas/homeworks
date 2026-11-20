using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private PlayerController _player;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameInstaller>();
        builder.RegisterComponentInHierarchy<SwitchInputButton>();

        builder.RegisterInstance(_movementConfig);
        builder.RegisterInstance(_player.transform);
        builder.Register<IMovementService, TransformMovementService>(Lifetime.Scoped);
    }
}
