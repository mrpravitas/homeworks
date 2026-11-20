using System;
using System.Collections.Generic;

public sealed class MessageBus : IMessageBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();

    public IDisposable Subscribe<T>(Action<T> handler)
    {
        var signalType = typeof(T);

        if (!_handlers.TryGetValue(signalType, out var handlers))
        {
            handlers = new List<Delegate>();
            _handlers.Add(signalType, handlers);
        }

        handlers.Add(handler);
        return new SignalSubscription(() => Unsubscribe(signalType, handler));
    }

    public void Publish<T>(T signal)
    {
        if (!_handlers.TryGetValue(typeof(T), out var handlers))
        {
            return;
        }

        var snapshot = handlers.ToArray();
        for (int i = 0; i < snapshot.Length; i++)
        {
            ((Action<T>)snapshot[i])(signal);
        }
    }

    private void Unsubscribe(Type signalType, Delegate handler)
    {
        if (!_handlers.TryGetValue(signalType, out var handlers))
        {
            return;
        }

        handlers.Remove(handler);
        if (handlers.Count == 0)
        {
            _handlers.Remove(signalType);
        }
    }
}
