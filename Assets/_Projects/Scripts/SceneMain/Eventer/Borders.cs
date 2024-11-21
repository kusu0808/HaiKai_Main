using BorderSystem;
using Sirenix.OdinInspector;
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

        [SerializeField, Tooltip("民家：ヤツの気配がする(1回目)")]
        private Border _houseFeelingYatsu1;
        public Border HouseFeelingYatsu1 => _houseFeelingYatsu1;

        [SerializeField, Tooltip("民家：ヤツの気配がする(2回目)")]
        private Border _houseFeelingYatsu2;
        public Border HouseFeelingYatsu2 => _houseFeelingYatsu2;

        [SerializeField, Tooltip("民家：ヤツの気配がする(3回目)")]
        private Border _houseFeelingYatsu3;
        public Border HouseFeelingYatsu3 => _houseFeelingYatsu3;

        [SerializeField, Tooltip("民家：廊下")]
        private MultiBorders _houseCorridor;
        public MultiBorders HouseCorridor => _houseCorridor;

        [SerializeField, Tooltip("民家：畳")]
        private MultiBorders _houseTatami;
        public MultiBorders HouseTatami => _houseTatami;

        [SerializeField, Tooltip("神社：参道だけ登れる角度が変わる")]
        private Border _enableGoUpOnShrineWay;
        public Border EnableGoUpOnShrineWay => _enableGoUpOnShrineWay;

        [SerializeField, Tooltip("神社：参道でヤツに見つかるイベントが始まる")]
        private Border _shrineWayFoundedEvent;
        public Border ShrineWayFoundedEvent => _shrineWayFoundedEvent;

        [SerializeField, Tooltip("舞台下：しゃがんで通り抜ける(3番目は、舞台下から参道に行くもの。イベントで通れなくなるため、これだけレイヤーが1。他は全て0。")]
        private TeleportBorders _underStageSquat;
        public TeleportBorders UnderStageSquat => _underStageSquat;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社上の道：ヤツが脱出経路の方から来る")]
        private Border _shrineUpWayYatsuComeFromEscapeRoute;
        public Border ShrineUpWayYatsuComeFromEscapeRoute => _shrineUpWayYatsuComeFromEscapeRoute;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道：ヤツが洞窟の方から来る")]
        private Border _villageFarWayYatsuComeFromCave;
        public Border VillageFarWayYatsuComeFromCave => _villageFarWayYatsuComeFromCave;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道：トイレに駆け込んで逃げ切る")]
        private Border _villageFarWayRunIntoToilet;
        public Border VillageFarWayRunIntoToilet => _villageFarWayRunIntoToilet;

        public bool IsFromUnderStageToShrineWayBorderEnabled { get; set; } = true;

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

                public Border GetBorder(bool isInBorder) => isInBorder ? In : Out;
                public Transform GetTransform(bool isInBorder) => isInBorder ? OutTf : InTf;
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

        /// <param name="layers">この中に入っていないものに関しては、判定を行わない(無視する)。空の場合は、レイヤーを考慮しないでメソッドの処理を行う</param>
        /// <returns>最初に見つけたもののインデックス、それ以外は-1</returns>
        public static int IsInAny(this ReadOnlyCollection<Borders.TeleportBorders.Element> elements, Vector3 pos, bool isInBorder, params int[] layers)
        {
            if (elements is null) return -1;
            if (layers is null) return -1;

            for (int i = 0; i < elements.Count; i++)
            {
                if (layers.Length <= 0)
                {
                    if (elements[i].GetBorder(isInBorder).IsIn(pos) is true) return i;
                }
                else
                {
                    foreach (int layer in layers)
                    {
                        if (elements[i].GetBorder(isInBorder).IsIn(pos, layer) is true) return i;
                    }
                }
            }

            return -1;
        }
    }
}