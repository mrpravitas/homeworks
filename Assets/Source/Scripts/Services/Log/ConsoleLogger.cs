using UnityEngine;
using VContainer;

public sealed class ConsoleLogger : ILogger
{
    [Inject]
    public void Construct()
    {
        DangerZone.OnEvent += Log;
        HealthPickup.OnEvent += Log;
    }

    public void Dispose()
    {
        DangerZone.OnEvent -= Log;
        HealthPickup.OnEvent -= Log;
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }
}
