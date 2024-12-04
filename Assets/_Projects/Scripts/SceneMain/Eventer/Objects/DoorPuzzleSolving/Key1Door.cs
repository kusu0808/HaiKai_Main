using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class Key1Door
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲")]
        private Border _border;
        public Border Border => _border;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵")]
        private Collider _key;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _door;

        public bool IsOpen => (_key == null) ? false : _key.gameObject.activeSelf;

        public void Trigger()
        {
            if (_key != null) _key.gameObject.SetActive(_key.gameObject.activeSelf);
            _door?.Trigger();
        }
    }
}