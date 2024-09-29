using UnityEngine;

namespace General
{
    public static class Log
    {
        public static void Tell(this string s, string colorCode = "ffffff")
        {
#if UNITY_EDITOR
            Debug.Log($"<color=#{colorCode}>{s}</color>");
#endif
        }

        public static void Show(this object obj)
        {
#if UNITY_EDITOR
            Debug.Log(obj);
#endif
        }

        public static void Warn(this object obj)
        {
#if UNITY_EDITOR
            Debug.LogWarning(obj);
#endif
        }

        public static void Error(this object obj)
        {
#if UNITY_EDITOR
            Debug.LogError(obj);
#endif
        }
    }
}