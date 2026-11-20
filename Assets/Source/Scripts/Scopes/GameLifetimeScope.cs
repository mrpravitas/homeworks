using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ILogger, ConsoleLogger>(Lifetime.Singleton);
        builder.Register<IMessageBus, MessageBus>(Lifetime.Singleton);
    }
}
