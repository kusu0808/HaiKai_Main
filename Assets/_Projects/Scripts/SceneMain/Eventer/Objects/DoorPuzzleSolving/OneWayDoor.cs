using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class OneWayDoor
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲(開く方)")]
        private Border _border;
        public Border Border => _border;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _door;

        public void Trigger()
        {
            _door?.Trigger();
        }
    }
}