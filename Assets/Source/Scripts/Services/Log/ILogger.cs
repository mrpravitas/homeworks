public interface ILogger : IDisposable
{
    void Init();
    void Log(string message);
}
