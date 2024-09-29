using System;
using UnityEngine;

namespace General
{
    public sealed class ScreenSetting
    {
        public Vector2Int Resolution { get; private set; }
        public bool IsFullScreen { get; private set; }
        public (bool isVsyncOn, int targetFrameRate) Display { get; private set; }

        public ScreenSetting(Vector2Int resolution, bool isFullScreen, (bool isVsyncOn, int targetFrameRate) display)
        {
            Resolution = resolution;
            IsFullScreen = isFullScreen;
            Display = display;
        }
    }

    [Serializable]
    public sealed class SerializedScreenSetting
    {
        [SerializeField, Header("解像度(ex. 1920, 1080)")]
        private Vector2Int _resolution;
        public Vector2Int Resolution
        {
            get
            {
                int x = Mathf.Clamp(_resolution.x, 960, 1920);
                int y = Mathf.Clamp(_resolution.y, 540, 1080);
                if (x * 9 != y * 16) (x, y) = (1920, 1080);
                return new(x, y);
            }
        }

        [SerializeField, Header("フルスクリーンか？")]
        private bool _isFullScreen;
        public bool IsFullScreen => _isFullScreen;

        [SerializeField, Header("垂直同期はオンか？")]
        private bool _isVsyncOn;
        [SerializeField, Range(30, 240), Header("垂直同期がオフなら、\nターゲットフレームレートはいくつか？")]
        private int _targetFrameRate;
        public (bool isVsyncOn, int targetFrameRate) Display => (_isVsyncOn, _targetFrameRate);
    }

    public static class ScreenSettingEx
    {
        public static void Apply(this ScreenSetting instance)
        {
            if (instance == null) return;

            Screen.SetResolution(instance.Resolution.x, instance.Resolution.y, instance.IsFullScreen);

            if (instance.Display.isVsyncOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = instance.Display.targetFrameRate;
            }
        }

        public static void Apply(this SerializedScreenSetting instance) => instance.Convert().Apply();

        private static ScreenSetting Convert(this SerializedScreenSetting instance) =>
            instance == null ? null : new(instance.Resolution, instance.IsFullScreen, instance.Display);
    }
}