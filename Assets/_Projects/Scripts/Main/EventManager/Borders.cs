using BorderSystem;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Main.EventManager
{
    [Serializable]
    public sealed class Borders
    {
        private ReadOnlyCollection<Border> _cache;
        [SerializeField, Tooltip("バス停：ここから先には行けない！")]
        private Border[] _busStopCannotMove;
        public ReadOnlyCollection<Border> BusStopCannotMove { get { _cache ??= Array.AsReadOnly(_busStopCannotMove); return _cache; } }

        [SerializeField, Tooltip("橋：きしむ音を立てる")]
        private Border _bridgePlaySound;
        public Border BridgePlaySound => _bridgePlaySound;

        [SerializeField, Tooltip("小道：しゃがんで通り抜ける 1")]
        private TeleportBorder _pathWaySquat1;
        public TeleportBorder PathWaySquat1 => _pathWaySquat1;

        [SerializeField, Tooltip("小道：しゃがんで通り抜ける 2")]
        private TeleportBorder _pathWaySquat2;
        public TeleportBorder PathWaySquat2 => _pathWaySquat2;

        [SerializeField, Tooltip("舞台下：しゃがんで通り抜ける")]
        private TeleportBorders _underStageSquat;
        public TeleportBorders UnderStageSquat => _underStageSquat;

        [Serializable]
        public sealed class TeleportBorder
        {
            [SerializeField]
            private Border _in;
            public Border In => _in;

            [SerializeField]
            private Border _out;
            public Border Out => _out;

            [SerializeField]
            private Transform _firstTf;
            public Transform FirstTf => _firstTf;

            [SerializeField]
            private Transform _secondTf;
            public Transform SecondTf => _secondTf;
        }

        [Serializable]
        public sealed class TeleportBorders
        {
            private ReadOnlyCollection<Element> _cache;

            [SerializeField]
            private Element[] _elements;
            public ReadOnlyCollection<Element> Elements { get { _cache ??= Array.AsReadOnly(_elements); return _cache; } }

            [Serializable]
            public sealed class Element
            {
                [SerializeField]
                private Border _in;

                [SerializeField]
                private Border _out;

                [SerializeField]
                private Transform _inTf;

                [SerializeField]
                private Transform _outTf;
            }
        }
    }

    public static class BordersEx
    {
        public static bool IsInAny(this ReadOnlyCollection<Border> borders, Vector3 pos)
        {
            if (borders is null) return false;
            foreach (Border border in borders) if (border.IsIn(pos) is true) return true;
            return false;
        }
    }
}