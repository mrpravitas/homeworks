using System;

public interface IMessageBus
{
    IDisposable Subscribe<T>(Action<T> handler);

    void Publish<T>(T signal);
}
