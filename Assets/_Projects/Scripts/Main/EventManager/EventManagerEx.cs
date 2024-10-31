using System;
using UnityEngine;

namespace Main.EventManager
{
    public static class EventManagerEx
    {
        public static string GetMessage(this string tag) => tag switch
        {
            "ActionEvent/BusSign" => "古びた標識だ",
            "ActionEvent/PathWaySign" => "汚れていて見えない",
            _ => string.Empty
        };
    }

    [Serializable]
    public sealed class Debug
    {
        [SerializeField, Tooltip("以下の全ての、設定の有効/無効")]
        private bool _isEnabled;
        public bool IsEnabled => _isEnabled;

        [SerializeField, Tooltip("プレイヤーの速度を5倍にする")]
        private bool _fastMove;
        public bool FastMove => _isEnabled && _fastMove;

        [SerializeField, Tooltip("プレイヤーの回転スピードを3倍にする")]
        private bool _fastLook;
        public bool FastLook => _isEnabled && _fastLook;
    }

    public static class EventManagerConst
    {
        public static readonly float FadeInDuration = 2;
        public static readonly float FadeOutDuration = 2;
        public static readonly float FadeInOutInterval = 1;
        public static readonly float NormalTextShowDuration = 3;
        public static readonly float EventTextShowDuration = 5;
        public static readonly float SameEventDuration = 5;
        public static readonly float RayMaxDistance = 2;
    }
}