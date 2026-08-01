using UnityEngine;

public class ConsoleLogger : ILogger
{
    public void Init()
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
