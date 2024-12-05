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
        private Collider _key1;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("予め鍵穴に刺しておいた鍵2")]
        private Collider _key2;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _door1;

        [SerializeField, Required, SceneObjectsOnly]
        private RotateDoor _door2;

        public void Unlock()
        {
            if (_key1 != null) _key1.gameObject.SetActive(true);
            if (_key2 != null) _key2.gameObject.SetActive(true);
            _door1?.Open();
            _door2?.Open();
        }
    }
}