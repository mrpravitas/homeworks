using System;

public sealed class SignalSubscription : IDisposable
{
    private Action _unsubscribe;

    public SignalSubscription(Action unsubscribe)
    {
        _unsubscribe = unsubscribe;
    }

    public void Dispose()
    {
        var unsubscribe = _unsubscribe;
        _unsubscribe = null;
        unsubscribe?.Invoke();
    }
}
