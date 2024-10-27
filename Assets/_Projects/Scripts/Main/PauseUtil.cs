using UnityEngine;

namespace Main
{
    public static class PauseState
    {
        private static bool _isPaused;
        public static bool IsPaused { get => _isPaused; set { _isPaused = value; Time.timeScale = value ? 0 : 1; } }
    }
}
