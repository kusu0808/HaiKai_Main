using System;
using UnityEngine;

namespace Main.EventManager
{
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
        public static readonly float FadeInDuration = 0.5f;
        public static readonly float FadeOutDuration = 0.5f;
        public static readonly float FadeInOutInterval = 0.2f;
        public static readonly float SameEventDuration = 5; // 同じ非ストーリーイベントの、発火クールタイム
        public static readonly float RayMaxDistance = 2;

        public static readonly float SlopLimitInit = 45.1f;
        public static readonly float SlopLimitOnStairs = 85;

        public static readonly int WalkingSoundBorderLayerGeneral = 0;
        public static readonly int WalkingSoundBorderLayerPathWayBridge = 1;
    }
}