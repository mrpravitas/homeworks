using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ILogger, ConsoleLogger>(Lifetime.Singleton);
        builder.Register<IMessageBus, MessageBus>(Lifetime.Singleton);

        builder.Register<KeyboardInputService>(Lifetime.Singleton);
        builder.Register<MouseInputService>(Lifetime.Singleton);
        builder.Register<IInputService, IInputSwitcher, InputSwitcher>(Lifetime.Singleton);
    }
}
