using NUnit.Framework;
using VContainer;

public sealed class MessageBusTests
{
    private readonly struct TestSignal
    {
    }

    [Test]
    public void Publish_AfterUnsubscribe_DoesNotCallHandler()
    {
        using var container = BuildContainer();
        var bus = container.Resolve<IMessageBus>();

        int calls = 0;
        void Handler(TestSignal signal) => calls++;

        var subscription = bus.Subscribe<TestSignal>(Handler);
        bus.Publish(new TestSignal());
        bus.Publish(new TestSignal());

        Assert.AreEqual(2, calls);

        subscription.Dispose();
        bus.Publish(new TestSignal());

        Assert.AreEqual(2, calls);
    }

    private static IObjectResolver BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.Register<IMessageBus, MessageBus>(Lifetime.Singleton);
        return builder.Build();
    }
}
