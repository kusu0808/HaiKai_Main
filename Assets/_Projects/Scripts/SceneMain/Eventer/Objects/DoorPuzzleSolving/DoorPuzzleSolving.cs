using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class DoorPuzzleSolvingClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Key1 _key1First;
        public Key1 Key1First => _key1First;

        [SerializeField, Required, SceneObjectsOnly]
        private OneWay _oneWay;
        public OneWay OneWay => _oneWay;

        [SerializeField, Required, SceneObjectsOnly]
        private Key1 _key1Second;
        public Key1 Key1Second => _key1Second;

        [SerializeField, Required, SceneObjectsOnly]
        private FinalKey2 _finalKey2;
        public FinalKey2 FinalKey2 => _finalKey2;
    }
}