using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class PickUpItems
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Collider _collider;

        public bool IsEnabled
        {
            get
            {
                if (_collider == null) return false;
                return _collider.gameObject.activeSelf;
            }
            set
            {
                if (_collider == null) return;
                _collider.gameObject.SetActive(value);
            }
        }
    }
}