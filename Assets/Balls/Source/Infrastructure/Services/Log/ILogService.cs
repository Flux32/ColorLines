namespace Balls.Source.Infrastructure.Services.Log
{
    public interface ILogService
    {
        bool Enabled { set; }
        void Log(string message);
        void LogError(string message);
        void LogWarning(string message);
    }
}