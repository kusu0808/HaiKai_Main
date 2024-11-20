using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Objects
    {
        [SerializeField, Required, SceneObjectsOnly]
        private BigIviesClass _bigIvies;
        public BigIviesClass BigIvies => _bigIvies;

        [SerializeField, Required, SceneObjectsOnly]
        private Collider _butaiSideKey;
        public bool IsButaiSideKeyEnabled
        {
            get
            {
                if (_butaiSideKey == null) return false;
                return _butaiSideKey.gameObject.activeSelf;
            }
            set
            {
                if (_butaiSideKey == null) return;
                _butaiSideKey.gameObject.SetActive(value);
            }
        }

        [Serializable]
        public sealed class BigIviesClass
        {
            public enum Type
            {
                PathWay,
                ShrineStair,
                CaveEntrance
            }

            [SerializeField, Required, SceneObjectsOnly, LabelText("PathWay")]
            private Collider _pathWay;

            [SerializeField, Required, SceneObjectsOnly, LabelText("ShrineStair")]
            private Collider _shrineStair;

            [SerializeField, Required, SceneObjectsOnly, LabelText("CaveEntrance")]
            private Collider _caveEntrance;

            public void DeactivateThis(Type type)
            {
                Collider collider = type switch
                {
                    Type.PathWay => _pathWay,
                    Type.ShrineStair => _shrineStair,
                    Type.CaveEntrance => _caveEntrance,
                    _ => null
                };
                if (collider == null) return;

                collider.gameObject.SetActive(false);
            }
        }
    }
}