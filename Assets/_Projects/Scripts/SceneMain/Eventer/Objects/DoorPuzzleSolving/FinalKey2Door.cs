using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class FinalKey2Door
    {
        public enum Type
        {
            Right,
            Left
        }

        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲")]
        private Border _border;
        public Border Border => _border;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵1")]
        private MeshRenderer _rightKey;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵2")]
        private MeshRenderer _leftKey;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _door1;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _door2;

        public bool IsOpenable() => _rightKey != null && _leftKey != null;

        public void Unlock(Type type)
        {
            var key = GetKey(type);
            if (key != null) return;
            key.enabled = true;
        }

        public void Trigger()
        {
            _door1?.Trigger();
            _door2?.Trigger();
        }

        private MeshRenderer GetKey(Type type) => type switch
        {
            Type.Right => _rightKey,
            Type.Left => _leftKey,
            _ => null
        };
    }
}