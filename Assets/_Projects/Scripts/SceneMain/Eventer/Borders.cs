using BorderSystem;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Borders
    {
        [SerializeField, Tooltip("バス停：ここから先には行けない！")]
        private MultiBorders _busStopCannotMove;
        public MultiBorders BusStopCannotMove => _busStopCannotMove;

        [SerializeField, Tooltip("小道：割れた皿を踏む")]
        private Border _footOnDish;
        public Border FootOnDish => _footOnDish;

        [SerializeField, Tooltip("小道：橋がきしむ音を立てる")]
        private Border _bridgePlaySound;
        public Border BridgePlaySound => _bridgePlaySound;

        [SerializeField, Tooltip("小道：しゃがんで通り抜ける 1")]
        private TeleportBorder _pathWaySquat1;
        public TeleportBorder PathWaySquat1 => _pathWaySquat1;

        [SerializeField, Tooltip("小道：しゃがんで通り抜ける 2")]
        private TeleportBorder _pathWaySquat2;
        public TeleportBorder PathWaySquat2 => _pathWaySquat2;

        [SerializeField, Tooltip("小道；行き止まり")]
        private Border _pathWayStop;
        public Border PathWayStop => _pathWayStop;

        [SerializeField, Tooltip("村道：鹿の鳴き声がする(1回目)")]
        private Border _villageWayDeerCry1;
        public Border VillageWayDeerCry1 => _villageWayDeerCry1;

        [SerializeField, Tooltip("村道：鹿の鳴き声がする(2回目)")]
        private Border _villageWayDeerCry2;
        public Border VillageWayDeerCry2 => _villageWayDeerCry2;

        [SerializeField, Tooltip("村道：鹿が飛び出す")]
        private Border _villageWayDeerJumpOut;
        public Border VillageWayDeerJumpOut => _villageWayDeerJumpOut;

        [SerializeField, Tooltip("村道：鳥が飛び立つ")]
        private Border _villageWayBirdFly;
        public Border VillageWayBirdFly => _villageWayBirdFly;

        [SerializeField, Tooltip("民家：入り口だけ昇れる角度が変わる")]
        private Border _enableGoUpOnEnteringHouse;
        public Border EnableGoUpOnEnteringHouse => _enableGoUpOnEnteringHouse;

        [SerializeField, Tooltip("舞台下：しゃがんで通り抜ける")]
        private TeleportBorders _underStageSquat;
        public TeleportBorders UnderStageSquat => _underStageSquat;

        [Serializable]
        public sealed class MultiBorders
        {
            private ReadOnlyCollection<Border> _cache;

            [SerializeField]
            private Border[] _elements;
            public ReadOnlyCollection<Border> Elements { get { _cache ??= Array.AsReadOnly(_elements); return _cache; } }
        }

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
                public Border In => _in;

                [SerializeField]
                private Border _out;
                public Border Out => _out;

                [SerializeField]
                private Transform _inTf;
                public Transform InTf => _inTf;

                [SerializeField]
                private Transform _outTf;
                public Transform OutTf => _outTf;
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

        public static int IsInInAny(this ReadOnlyCollection<Borders.TeleportBorders.Element> elements, Vector3 pos)
        {
            if (elements is null) return -1;
            for (int i = 0; i < elements.Count; i++) if (elements[i].In.IsIn(pos) is true) return i;
            return -1;
        }

        public static int IsInOutAny(this ReadOnlyCollection<Borders.TeleportBorders.Element> elements, Vector3 pos)
        {
            if (elements is null) return -1;
            for (int i = 0; i < elements.Count; i++) if (elements[i].Out.IsIn(pos) is true) return i;
            return -1;
        }
    }
}