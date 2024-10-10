using UnityEngine;

namespace General
{
    public static class Log
    {
        public static string Tell(this string s, string colorCode = "ffffff")
        {
#if UNITY_EDITOR
            Debug.Log($"<color=#{colorCode}>{s}</color>");
            return s;
#endif
        }

        public static T Show<T>(this T obj)
        {
#if UNITY_EDITOR
            Debug.Log(obj);
            return obj;
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