using UnityEngine;

namespace Balls.Source.Infrastructure.Services.Log
{
    public class LogService : ILogService
    {
        public bool Enabled
        {
            set => Debug.unityLogger.logEnabled = value;
        }
        
        public void Log(string message)
        {
            Debug.Log(CreateMessage(message, Color.green));
        }

        public void LogError(string message)
        {
            Debug.LogError(CreateMessage(message, Color.red));
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(CreateMessage(message, Color.yellow));
        }

        private string CreateMessage(string message, Color color)
        {
            return $"<color={ColorToHex(color)}>{message} </color>";
        }

        private string ColorToHex(Color color)
        {
            int r = Mathf.FloorToInt(color.r * 255);
            int g = Mathf.FloorToInt(color.g * 255);
            int b = Mathf.FloorToInt(color.b * 255);

            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}