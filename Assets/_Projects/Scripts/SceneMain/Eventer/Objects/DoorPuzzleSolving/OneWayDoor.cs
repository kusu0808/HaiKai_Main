using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class OneWayDoor
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲1(開かない方)")]
        private Border _border1;
        public Border Border1 => _border1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲2(開く方)")]
        private Border _border2;
        public Border Border2 => _border2;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _door;

        public void Unlock()
        {
            _door?.Open();
        }
    }
}