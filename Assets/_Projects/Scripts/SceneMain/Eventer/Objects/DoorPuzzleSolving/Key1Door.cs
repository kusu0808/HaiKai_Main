using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class Key1Door
    {
        public enum Type
        {
            First,
            Second
        }

        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲")]
        private Border _border;
        public Border Border => _border;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵")]
        private MeshRenderer _key;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _door;

        public bool IsMoving => _door?.IsMoving ?? default;

        public bool IsOpen => (_key == null) ? false : _key.enabled;

        public void Trigger()
        {
            if (_key != null) _key.enabled = !_key.enabled;
            _door?.Trigger();
        }
    }
}