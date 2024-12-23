using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Main.Eventer.Borders
{
    [Serializable]
    public sealed class WalkingSounds
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("割れた皿(初回以外)")]
        private MultiBorders _brokenDish;
        public MultiBorders BrokenDish => _brokenDish;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("橋")]
        private MultiBorders _bridge;
        public MultiBorders Bridge => _bridge;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("廊下")]
        private MultiBorders _corridor;
        public MultiBorders Corridor => _corridor;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("畳")]
        private MultiBorders _tatami;
        public MultiBorders Tatami => _tatami;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("道路")]
        private MultiBorders _road;
        public MultiBorders Road => _road;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("石の階段")]
        private MultiBorders _stoneStairs;
        public MultiBorders StoneStairs => _stoneStairs;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("土")]
        private MultiBorders _soil;
        public MultiBorders Soil => _soil;
    }
}