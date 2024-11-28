using UnityEngine;

namespace General
{
    public static class Ex
    {
        /// <summary>
        /// X[a, b]をY[c, d]に線形変換する
        /// </summary>
        public static float Remap(this float x, float a, float b, float c, float d)
            => a == b ? 0 : (x - a) * (d - c) / (b - a) + c;

        public static Vector2 WithoutY(this Vector3 v, out float ignoredY)
        {
            ignoredY = v.y;
            return new(v.x, v.z);
        }

        public static Vector3 WithY(this Vector2 v, float y) => new(v.x, y, v.y);
    }
}