using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using VContainer;

public sealed class HealthServiceTests
{
    private sealed class TestLogger : ILogger
    {
        public readonly List<string> Messages = new List<string>();

        public void Log(string message)
        {
            Messages.Add(message);
        }

        public void Dispose()
        {
        }
    }

    private sealed class TestHealthPresenter : IHealthPresenter
    {
        public int LastCurrent;
        public int LastMax;
        public int Calls;

        public void OnHealthChanged(int current, int max)
        {
            LastCurrent = current;
            LastMax = max;
            Calls++;
        }
    }

    [Test]
    public void Signals_ChangeHealth_AndRaiseDied()
    {
        var config = ScriptableObject.CreateInstance<HealthConfig>();
        var serialized = new SerializedObject(config);
        serialized.FindProperty("_maxHealth").intValue = 100;
        serialized.ApplyModifiedPropertiesWithoutUndo();

        var logger = new TestLogger();
        var presenter = new TestHealthPresenter();

        var builder = new ContainerBuilder();
        builder.RegisterInstance(config);
        builder.RegisterInstance<ILogger>(logger);
        builder.RegisterInstance<IHealthPresenter>(presenter);
        builder.Register<IMessageBus, MessageBus>(Lifetime.Singleton);
        builder.Register<HealthService>(Lifetime.Singleton);

        using var container = builder.Build();
        var health = container.Resolve<HealthService>();
        var bus = container.Resolve<IMessageBus>();

        int deaths = 0;
        health.OnDied += () => deaths++;

        Assert.AreEqual(100, health.Current);
        Assert.AreEqual(100, health.Max);

        bus.Publish(new PlayerDamagedSignal(30, "test"));
        Assert.AreEqual(70, health.Current);
        Assert.AreEqual(70, presenter.LastCurrent);
        Assert.AreEqual(100, presenter.LastMax);
        Assert.AreEqual(0, deaths);

        bus.Publish(new HealthPickupCollectedSignal(20));
        Assert.AreEqual(90, health.Current);
        Assert.AreEqual(90, presenter.LastCurrent);

        bus.Publish(new HealthPickupCollectedSignal(50));
        Assert.AreEqual(100, health.Current);
        Assert.AreEqual(100, presenter.LastCurrent);

        bus.Publish(new PlayerDamagedSignal(150, "test"));
        Assert.AreEqual(0, health.Current);
        Assert.AreEqual(1, deaths);
        Assert.IsTrue(logger.Messages.Contains("You died."));
    }
}
