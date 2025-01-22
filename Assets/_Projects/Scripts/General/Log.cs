#if UNITY_EDITOR

using UnityEngine;

namespace General
{
    public static class Log
    {
        public static string Tell(this string s, string colorCode = "ffffff")
        {
            Debug.Log($"<color=#{colorCode}>{s}</color>");
            return s;
        }

        public static T Show<T>(this T obj)
        {
            Debug.Log(obj);
            return obj;
        }

        public static void Warn(this object obj)
        {
            Debug.LogWarning(obj);
        }

        public static void Error(this object obj)
        {
            Debug.LogError(obj);
        }
    }
}

#endif