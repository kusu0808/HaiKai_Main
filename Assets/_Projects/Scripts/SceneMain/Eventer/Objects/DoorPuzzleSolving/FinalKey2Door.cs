using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class FinalKey2Door
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲")]
        private Border _border;
        public Border Border => _border;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵1")]
        private MeshRenderer _key1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵2")]
        private MeshRenderer _key2;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _door1;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _door2;

        public bool IsOpenable() => _key1 != null && _key2 != null;

        public void Unlock(bool isKey)
        {
            var key = isKey ? _key1 : _key2;
            if (key != null) return;
            key.enabled = true;
        }

        public void Trigger()
        {
            _door1?.Trigger();
            _door2?.Trigger();
        }
    }
}