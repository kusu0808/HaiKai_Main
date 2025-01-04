using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class DoorPuzzleSolvingClass
    {
        public enum Key1DoorType
        {
            First,
            Second
        }

        public enum FinalKey2DoorType
        {
            First,
            Second
        }

        [SerializeField, Required, SceneObjectsOnly]
        private Key1Door _key1First;
        public Key1Door Key1First => _key1First;

        [SerializeField, Required, SceneObjectsOnly]
        private OneWayDoor _oneWay;
        public OneWayDoor OneWay => _oneWay;

        [SerializeField, Required, SceneObjectsOnly]
        private Key1Door _key1Second;
        public Key1Door Key1Second => _key1Second;

        [SerializeField, Required, SceneObjectsOnly]
        private FinalKey2Door _finalKey2;
        public FinalKey2Door FinalKey2 => _finalKey2;

        public Key1Door GetKey1Door(Key1DoorType type) => type switch
        {
            Key1DoorType.First => _key1First,
            Key1DoorType.Second => _key1Second,
            _ => null
        };
    }
}