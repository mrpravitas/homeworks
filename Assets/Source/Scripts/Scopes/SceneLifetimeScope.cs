using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private HealthConfig _healthConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameInstaller>();
        builder.RegisterComponentInHierarchy<SwitchInputButton>();
        builder.RegisterComponentInHierarchy<PlayerController>();

        builder.RegisterInstance(_movementConfig);
        builder.RegisterInstance(_healthConfig);
        builder.Register<Transform>(
            resolver => resolver.Resolve<PlayerController>().transform,
            Lifetime.Scoped);

        builder.Register<IMovementService, TransformMovementService>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<UITextHealthPresenter>().As<IHealthPresenter>();
        builder.Register<IHealthService, HealthService>(Lifetime.Scoped);
    }
}
