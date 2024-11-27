namespace General
{
    public static class Ex
    {
        /// <summary>
        /// X[a, b]をY[c, d]に線形変換する
        /// </summary>
        public static float Remap(this float x, float a, float b, float c, float d)
            => a == b ? 0 : (x - a) * (d - c) / (b - a) + c;
    }
}