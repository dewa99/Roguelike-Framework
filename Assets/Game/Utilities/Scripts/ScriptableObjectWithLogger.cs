using UnityEngine;

namespace RoguelikeCardSystem.Game.Utilities
{
    public class ScriptableObjectWithLogger : ScriptableObject
    {
        protected void Log(object message, LogOption option = LogOption.NoStacktrace)
        {
            Debug.LogFormat(LogType.Log, option, this, $"[{name}] {message}");
        }

        protected void LogWarning(object message, LogOption option = LogOption.NoStacktrace)
        {
            Debug.LogFormat(LogType.Warning, option, this, $"[{name}] {message}");
        }

        protected void LogError(object message, LogOption option = LogOption.NoStacktrace)
        {
            Debug.LogFormat(LogType.Error, option, this, $"[{name}] {message}");
        }
    }
}
