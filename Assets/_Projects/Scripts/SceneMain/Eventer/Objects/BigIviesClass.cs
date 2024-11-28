using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
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