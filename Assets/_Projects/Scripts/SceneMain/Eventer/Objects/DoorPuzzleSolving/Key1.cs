using System;
using BorderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class Key1
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("アクションできる範囲")]
        private Border _border;
        public Border Border => _border;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵")]
        private Collider _key;

        [SerializeField, Required, SceneObjectsOnly]
        private SlideDoor _door;

        public void Unlock()
        {
            if (_key != null) _key.gameObject.SetActive(true);
            _door.Open();
        }
    }
}