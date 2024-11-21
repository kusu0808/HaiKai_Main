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

        [SerializeField, Required, SceneObjectsOnly]
        private Collider _toiletCup;
        public bool IsToiletCupEnabled
        {
            get
            {
                if (_toiletCup == null) return false;
                return _toiletCup.gameObject.activeSelf;
            }
            set
            {
                if (_toiletCup == null) return;
                _toiletCup.gameObject.SetActive(value);
            }
        }

        [SerializeField, Required, SceneObjectsOnly]
        private Collider _toiletOneWayDoor;
        public bool IsToiletOneWayDoorEnabled
        {
            get
            {
                if (_toiletOneWayDoor == null) return false;
                return _toiletOneWayDoor.gameObject.activeSelf;
            }
            set
            {
                if (_toiletOneWayDoor == null) return;
                _toiletOneWayDoor.gameObject.SetActive(value);
            }
        }

        [SerializeField, Required, SceneObjectsOnly]
        private Collider _warehouseLockedDoor;
        public bool IsWarehouseLockedDoorEnabled
        {
            get
            {
                if (_warehouseLockedDoor == null) return false;
                return _warehouseLockedDoor.gameObject.activeSelf;
            }
            set
            {
                if (_warehouseLockedDoor == null) return;
                _warehouseLockedDoor.gameObject.SetActive(value);
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