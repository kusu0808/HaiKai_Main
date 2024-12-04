using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class OneWay
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲1")]
        private Border _border1;
        public Border Border1 => _border1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲2")]
        private Border _border2;
        public Border Border2 => _border2;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _door;

        public void Unlock()
        {
            _door.Open();
        }
    }
}