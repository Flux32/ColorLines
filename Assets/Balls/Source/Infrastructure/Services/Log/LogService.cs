using UnityEngine;

namespace Balls.Source.Infrastructure.Services.Log
{
    public class LogService : MonoBehaviour, ILogService
    {
        public bool Enabled
        {
            set => Debug.unityLogger.logEnabled = value;
        }
    
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
    }
}