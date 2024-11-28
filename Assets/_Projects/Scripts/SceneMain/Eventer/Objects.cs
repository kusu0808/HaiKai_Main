using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Main.Eventer.MovableDoor;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Objects
    {
        [SerializeField, Required, SceneObjectsOnly]
        private BigIviesClass _bigIvies;
        public BigIviesClass BigIvies => _bigIvies;

        [SerializeField, Required, SceneObjectsOnly]
        private DeersClass _deers;
        public DeersClass Deers => _deers;

        [SerializeField, Required, SceneObjectsOnly]
        private PickUpItems _butaiSideKey;
        public PickUpItems ButaiSideKey => _butaiSideKey;

        [SerializeField, Required, SceneObjectsOnly]
        private PickUpItems _toiletCup;
        public PickUpItems ToiletCup => _toiletCup;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _toiletClosedDoor;
        public AMovableDoor ToiletOneWayDoor => _toiletClosedDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _warehouseLookedDoor;
        public AMovableDoor WarehouseLookedDoor => _warehouseLookedDoor;
    }
}