using System;

public interface ILogger : IDisposable
{
    void Log(string message);
}
