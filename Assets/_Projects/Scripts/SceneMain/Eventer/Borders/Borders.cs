using BorderSystem;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Main.Eventer.Borders
{
    [Serializable]
    public sealed class Borders
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("バス停：ここから先には行けない！")]
        private MultiBorders _busStopCannotMove;
        public MultiBorders BusStopCannotMove => _busStopCannotMove;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道：割れた皿を踏む")]
        private Border _footOnDish;
        public Border FootOnDish => _footOnDish;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道：橋がきしむ音を立てる")]
        private Border _bridgePlaySound;
        public Border BridgePlaySound => _bridgePlaySound;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道：しゃがんで通り抜ける 1")]
        private TeleportBorder _pathWaySquat1;
        public TeleportBorder PathWaySquat1 => _pathWaySquat1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道：しゃがんで通り抜ける 2")]
        private TeleportBorder _pathWaySquat2;
        public TeleportBorder PathWaySquat2 => _pathWaySquat2;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道；行き止まり")]
        private Border _pathWayStop;
        public Border PathWayStop => _pathWayStop;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("村道：鹿の鳴き声がする(1回目)")]
        private Border _villageWayDeerCry1;
        public Border VillageWayDeerCry1 => _villageWayDeerCry1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("村道：鹿の鳴き声がする(2回目)")]
        private Border _villageWayDeerCry2;
        public Border VillageWayDeerCry2 => _villageWayDeerCry2;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("村道：鹿が飛び出す")]
        private Border _villageWayDeerJumpOut;
        public Border VillageWayDeerJumpOut => _villageWayDeerJumpOut;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("村道：鳥が飛び立つ")]
        private Border _villageWayBirdFly;
        public Border VillageWayBirdFly => _villageWayBirdFly;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("民家：入り口だけ昇れる角度が変わる")]
        private Border _enableGoUpOnEnteringHouse;
        public Border EnableGoUpOnEnteringHouse => _enableGoUpOnEnteringHouse;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("民家：ヤツの気配がする(1回目)")]
        private Border _houseFeelingYatsu1;
        public Border HouseFeelingYatsu1 => _houseFeelingYatsu1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("民家：ヤツの気配がする(2回目)")]
        private Border _houseFeelingYatsu2;
        public Border HouseFeelingYatsu2 => _houseFeelingYatsu2;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("民家：ヤツの気配がする(3回目)")]
        private Border _houseFeelingYatsu3;
        public Border HouseFeelingYatsu3 => _houseFeelingYatsu3;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("民家：廊下")]
        private MultiBorders _houseCorridor;
        public MultiBorders HouseCorridor => _houseCorridor;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("民家：畳")]
        private MultiBorders _houseTatami;
        public MultiBorders HouseTatami => _houseTatami;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社：参道だけ登れる角度が変わる")]
        private Border _enableGoUpOnShrineWay;
        public Border EnableGoUpOnShrineWay => _enableGoUpOnShrineWay;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社：参道でヤツに見つかるイベントが始まる")]
        private Border _shrineWayFoundedEvent;
        public Border ShrineWayFoundedEvent => _shrineWayFoundedEvent;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("舞台下：しゃがんで通り抜ける(3番目は、舞台下から参道に行くもの。イベントで通れなくなるため、これだけレイヤーが1。他は全て0。")]
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

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道：ヤツがトイレを叩く音が止む")]
        private Border _villageFarWayYatsuStopToiletSound;
        public Border VillageFarWayYatsuStopToiletSound => _villageFarWayYatsuStopToiletSound;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("倉庫：階段だけ登れる角度が変わる")]
        private Border _enableGoUpOnWarehouseStairs;
        public Border EnableGoUpOnWarehouseStairs => _enableGoUpOnWarehouseStairs;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("倉庫：鹿が落下する")]
        private Border _warehouseDeerFall;
        public Border WarehouseDeerFall => _warehouseDeerFall;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("村道：倉庫から出てきた後、引き返せなくなる")]
        private Border _villageWayCannotGoBackAfterWarehouse;
        public Border VillageWayCannotGoBackAfterWarehouse => _villageWayCannotGoBackAfterWarehouse;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道：洞窟に向かうとき、ヤツが娘の声真似をする")]
        private Border _villageFarWayYatsuDaughterVoice;
        public Border VillageFarWayYatsuDaughterVoice => _villageFarWayYatsuDaughterVoice;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道：ガラス片を撒ける")]
        private Border _villageFarWayScatterGlassPiece;
        public Border VillageFarWayScatterGlassPiece => _villageFarWayScatterGlassPiece;

        public bool IsFromUnderStageToShrineWayBorderEnabled { get; set; } = true;
    }
}