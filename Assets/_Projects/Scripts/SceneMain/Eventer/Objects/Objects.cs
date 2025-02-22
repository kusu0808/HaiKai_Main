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
        private PlacedItemClass _daughterKnife;
        public PlacedItemClass DaughterKnife => _daughterKnife;

        [SerializeField, Required, SceneObjectsOnly]
        private BigIviesClass _bigIvies;
        public BigIviesClass BigIvies => _bigIvies;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _shrineWayRock;
        public PlacedItemClass ShrineWayRock => _shrineWayRock;

        [SerializeField, Required, SceneObjectsOnly]
        private DeersClass _deers;
        public DeersClass Deers => _deers;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _warehouseKeyDoubled;
        public PlacedItemClass WarehouseKeyDoubled => _warehouseKeyDoubled;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _toiletCup;
        public PlacedItemClass ToiletCup => _toiletCup;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _toiletLockedDoor;
        public AMovableDoor<RotateDoor> ToiletLockedDoor => _toiletLockedDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _toiletOneWayDoor;
        public AMovableDoor<RotateDoor> ToiletOneWayDoor => _toiletOneWayDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _warehouseLockedDoor;
        public AMovableDoor<SlideDoor> WarehouseLockedDoor => _warehouseLockedDoor;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _warehouseOneWayDoor;
        public AMovableDoor<SlideDoor> WarehouseOneWayDoor => _warehouseOneWayDoor;

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
        private PlacedItemClass _keyInDoorPuzzleSolving;
        public PlacedItemClass KeyInDoorPuzzleSolving => _keyInDoorPuzzleSolving;

        [SerializeField, Required, SceneObjectsOnly]
        private DoorPuzzleSolvingClass _doorPuzzleSolving;
        public DoorPuzzleSolvingClass DoorPuzzleSolving => _doorPuzzleSolving;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _villageFarWayScatteredGlassPiece;
        public PlacedItemClass VillageFarWayScatteredGlassPiece => _villageFarWayScatteredGlassPiece;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _shrineChainedDaughter;
        public PlacedItemClass ShrineChainedDaughter => _shrineChainedDaughter;

        [SerializeField, Required, SceneObjectsOnly]
        private DaughterChainClass _daughterChain;
        public DaughterChainClass DaughterChain => _daughterChain;

        [SerializeField, Required, SceneObjectsOnly]
        private BlockingVolumeClass _shrineCannotGetOutUntilDaughterSaved;
        public BlockingVolumeClass ShrineCannotGetOutUntilDaughterSaved => _shrineCannotGetOutUntilDaughterSaved;

        [SerializeField, Required, SceneObjectsOnly]
        private BlockingVolumeClass _shrineUpWayCannotGoAtLastEscape;
        public BlockingVolumeClass ShrineUpWayCannotGoAtLastEscape => _shrineUpWayCannotGoAtLastEscape;

        [SerializeField, Required, SceneObjectsOnly]
        private BlockingVolumeClass _pathWayCannotGoAtLastEscape;
        public BlockingVolumeClass PathWayCannotGoAtLastEscape => _pathWayCannotGoAtLastEscape;

        [SerializeField, Required, SceneObjectsOnly]
        private TimelineClass _shrineWayFoundByYatsuTimeline;
        public TimelineClass ShrineWayFoundByYatsuTimeline => _shrineWayFoundByYatsuTimeline;

        [SerializeField, Required, SceneObjectsOnly]
        private TimelineClass _shrineWayYatsuComeAtLastEscapeTimeline;
        public TimelineClass ShrineWayYatsuComeAtLastEscapeTimeline => _shrineWayYatsuComeAtLastEscapeTimeline;

        [SerializeField, Required, SceneObjectsOnly]
        private TimelineClass _busStopEscapeTimeline;
        public TimelineClass BusStopEscapeTimeline => _busStopEscapeTimeline;

        [SerializeField, Required, SceneObjectsOnly]
        private EndingCutSceneClass _endingCutScene;
        public EndingCutSceneClass EndingCutScene => _endingCutScene;
    }
}