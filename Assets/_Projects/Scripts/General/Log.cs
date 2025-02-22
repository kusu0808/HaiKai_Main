using UnityEngine;

namespace General
{
    public static class Log
    {
        private const string Symbol = "ENABLE_LOG";

        [System.Diagnostics.Conditional(Symbol)]
        public static void Tell(this string s, string colorCode = "ffffff") => Debug.Log($"<color=#{colorCode}>{s}</color>");

        [System.Diagnostics.Conditional(Symbol)]
        public static void Warn(this object obj) => Debug.LogWarning(obj);

        [System.Diagnostics.Conditional(Symbol)]
        public static void Error(this object obj) => Debug.LogError(obj);
    }
}