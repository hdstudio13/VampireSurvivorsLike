using System.Diagnostics;

namespace Debug
{
    public static class GameLogger
    {
        private const string CONDITION_STRING = "UNITY_EDITOR";

        [Conditional(CONDITION_STRING)]
        public static void Log(this object obj, string message)
        {
            Log($"[{obj.GetType().Name}] {message}");
        }

        [Conditional(CONDITION_STRING)]
        public static void LogError(this object obj, string message)
        {
            LogError($"[{obj.GetType().Name}] {message}");
        }

        [Conditional(CONDITION_STRING)]
        public static void LogWarning(this object obj, string message)
        {
            LogWarning($"[{obj.GetType().Name}] {message}");
        }

        [Conditional(CONDITION_STRING)]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        [Conditional(CONDITION_STRING)]
        public static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        [Conditional(CONDITION_STRING)]
        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }
    }
}