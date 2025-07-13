using UnityEngine;

namespace RoguelikeCardSystem.Game.Utilities
{
    public class ClassWithLogger
    {
        protected void Log(object message, LogOption option = LogOption.NoStacktrace)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var prefix = $"[{GetType().Name}]";
            var metadata = $"[{GetHashCode()}][{Time.time.ToFormattedTimeString()}]";
            Debug.LogFormat(LogType.Log, option, null, $"{prefix} {message} {metadata}");
#endif
        }

        protected void LogWarning(object message, LogOption option = LogOption.NoStacktrace)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var prefix = $"[{GetType().Name}][WARNING]";
            var metadata = $"[{GetHashCode()}][{Time.time.ToFormattedTimeString()}]";
            Debug.LogFormat(LogType.Warning, option, null, $"{prefix} {message} {metadata}");
#endif
        }

        protected void LogError(object message, LogOption option = LogOption.NoStacktrace)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var prefix = $"[{GetType().Name}][ERROR]";
            var metadata = $"[{GetHashCode()}][{Time.time.ToFormattedTimeString()}]";
            Debug.LogFormat(LogType.Error, option, null, $"{prefix} {message} {metadata}");
#endif
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <typeparam name="T">The type of the class calling the method.</typeparam>
        protected static void Log<T>(object message, LogOption option = LogOption.NoStacktrace) where T : ClassWithLogger
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogFormat(LogType.Log, option, null, $"[{typeof(T)}]{message}[{Time.time.ToFormattedTimeString()}]");
#endif
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <typeparam name="T">The type of the class calling the method.</typeparam>
        protected static void LogWarning<T>(object message, LogOption option = LogOption.NoStacktrace) where T : ClassWithLogger
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogFormat(LogType.Warning, option, null, $"[{typeof(T)}][WARNING]{message}[{Time.time.ToFormattedTimeString()}]");
#endif
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <typeparam name="T">The type of the class calling the method.</typeparam>
        protected static void LogError<T>(object message, LogOption option = LogOption.NoStacktrace) where T : ClassWithLogger
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogFormat(LogType.Error, option, null, $"[{typeof(T)}][ERROR]{message}[{Time.time.ToFormattedTimeString()}]");
#endif
        }
    }
}
