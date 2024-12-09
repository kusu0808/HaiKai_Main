using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Main.Eventer.Objects.DoorPuzzleSolving;

namespace Main.Eventer.Objects
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
        private PlacedItemClass _warehouseeKey;
        public PlacedItemClass WarehouseKey => _warehouseeKey;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _toiletCup;
        public PlacedItemClass ToiletCup => _toiletCup;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _toiletOneWayDoor;
        public AMovableDoor ToiletOneWayDoor => _toiletOneWayDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _warehouseLookedDoor;
        public AMovableDoor WarehouseLookedDoor => _warehouseLookedDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _warehouseOneWayDoor;
        public AMovableDoor WarehouseOneWayDoor => _warehouseOneWayDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private BlockingVolumeClass _villageWayCannotGoBackAfterWarehouse;
        public BlockingVolumeClass VillageWayCannotGoBackAfterWarehouse => _villageWayCannotGoBackAfterWarehouse;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _kokeshiHead;
        public PlacedItemClass KokeshiHead => _kokeshiHead;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _kokeshiSecretKey;
        public PlacedItemClass KokeshiSecretKey => _kokeshiSecretKey;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _glassShard;
        public PlacedItemClass GlassShard => _glassShard;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _keyInDoorPuzzleSolving;
        public PlacedItemClass KeyInDoorPuzzleSolving => _keyInDoorPuzzleSolving;

        [SerializeField, Required, SceneObjectsOnly]
        private DoorPuzzleSolvingClass _doorPuzzleSolving;
        public DoorPuzzleSolvingClass DoorPuzzleSolving => _doorPuzzleSolving;

        [SerializeField, Required, SceneObjectsOnly]
        private GokiChanClass _gokiChan;
        public GokiChanClass GokiChan => _gokiChan;
    }
}