using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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

        [SerializeField, Required, SceneObjectsOnly, FormerlySerializedAs("_key1"), Tooltip("予め鍵穴に刺しておいた鍵1")]
        private MeshRenderer _keyRight;

        [SerializeField, Required, SceneObjectsOnly, FormerlySerializedAs("_key2"), Tooltip("予め鍵穴に刺しておいた鍵2")]
        private MeshRenderer _keyLeft;

        [SerializeField, Required, FormerlySerializedAs("_door1"), SceneObjectsOnly]
        private RotateDoor _doorRight;

        [SerializeField, Required, FormerlySerializedAs("_door2"), SceneObjectsOnly]
        private RotateDoor _doorLeft;

        public bool IsMoving(Type type) => GetDoor(type)?.IsMoving ?? default;

        public bool IsOpen(Type type)
        {
            MeshRenderer key = GetKey(type);
            if (key == null) return true;
            return key.enabled;
        }

        public bool IsOpenBoth => IsOpen(Type.Right) && IsOpen(Type.Left);

        public void SetKey(Type type, bool isActive)
        {
            MeshRenderer key = GetKey(type);
            if (key == null) return;
            key.enabled = isActive;
        }

        public void Trigger()
        {
            _doorRight?.Trigger();
            _doorLeft?.Trigger();
        }

        private MeshRenderer GetKey(Type type) => type switch
        {
            Type.Right => _keyRight,
            Type.Left => _keyLeft,
            _ => null
        };

        private RotateDoor GetDoor(Type type) => type switch
        {
            Type.Right => _doorRight,
            Type.Left => _doorLeft,
            _ => null
        };
    }
}