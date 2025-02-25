using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Points
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("落下などで強制引き戻しする場合も、ここに戻す")]
        private Transform _init;
        public Transform Init => _init;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("ゲームの最初で、娘が出現する場所")]
        private Transform _roadWayDaughterSpawnPoint;
        public Transform RoadWayDaughterSpawnPoint => _roadWayDaughterSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社の道でヤツに見つかるイベントで、ヤツが出現する場所")]
        private Transform _shrineWayYatsuSpawnPoint;
        public Transform ShrineWayYatsuSpawnPoint => _shrineWayYatsuSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社の道でヤツに見つかるイベントで、プレイヤーがテレポートする場所")]
        private Transform _shrineWayPlayerTeleportPoint;
        public Transform ShrineWayPlayerTeleportPoint => _shrineWayPlayerTeleportPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社上の道で、ヤツが脱出経路から来る時の、スポーン場所")]
        private Transform _shrineUpWayYatsuComeFromEscapeRouteSpawnPoint;
        public Transform ShrineUpWayYatsuComeFromEscapeRouteSpawnPoint => _shrineUpWayYatsuComeFromEscapeRouteSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道で、ヤツが洞窟の方から来る時の、スポーン場所")]
        private Transform _villageFarWayYatsuComeFromCaveSpawnPoint;
        public Transform VillageFarWayYatsuComeFromCaveSpawnPoint => _villageFarWayYatsuComeFromCaveSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道で、鍵を使ってトイレのドアを開けた後の、テレポート先")]
        private Transform _villageFarWayInsideToiletPoint;
        public Transform VillageFarWayInsideToiletPoint => _villageFarWayInsideToiletPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道で、謎を解いてトイレのドアを開けた後の、テレポート先")]
        private Transform _villageFarWayOutsideToiletPoint;
        public Transform VillageFarWayOutsideToiletPoint => _villageFarWayOutsideToiletPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("奥の村道で、洞窟の蔦を切った時の、ヤツのスポーン場所")]
        private Transform _villageFarWayOnCutIvyYatsuSpawnPoint;
        public Transform VillageFarWayOnCutIvyYatsuSpawnPoint => _villageFarWayOnCutIvyYatsuSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社で、鎖を切った時の、娘が出現する場所")]
        private Transform _shrineDaughterSpawnPoint;
        public Transform ShrineDaughterSpawnPoint => _shrineDaughterSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社上の道で、最後の脱出時にヤツが現れる時の、スポーン場所")]
        private Transform _shrineUpWayYatsuComeAtLastEscapeSpawnPoint;
        public Transform ShrineUpWayYatsuComeAtLastEscapeSpawnPoint => _shrineUpWayYatsuComeAtLastEscapeSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道で、最後の脱出時にヤツが現れる時の、スポーン場所")]
        private Transform _pathWayYatsuComeAtLastEscapeSpawnPoint;
        public Transform PathWayYatsuComeAtLastEscapeSpawnPoint => _pathWayYatsuComeAtLastEscapeSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("神社上の道で、最後の脱出時にヤツが現れる時の、 娘のスポーン場所")]
        private Transform _shrineUpWayDaughterAtLastEscapeSpawnPoint;
        public Transform ShrineUpWayDaughterAtLastEscapeSpawnPoint => _shrineUpWayDaughterAtLastEscapeSpawnPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道で、最後の脱出時にプレイヤーが飛び降りる時の、プレイヤーのテレポート場所")]
        private Transform _pathWayPlayerTeleportPointAtLastEscape;
        public Transform PathWayPlayerTeleportPointAtLastEscape => _pathWayPlayerTeleportPointAtLastEscape;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("小道で、最後の脱出時にヤツが現れる時の、 娘のスポーン場所")]
        private Transform _pathWayDaughterAtLastEscapeSpawnPoint;
        public Transform PathWayDaughterAtLastEscapeSpawnPoint => _pathWayDaughterAtLastEscapeSpawnPoint;
    }
}