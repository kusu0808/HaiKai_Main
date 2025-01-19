using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.UIElements
{
    [Serializable]
    public sealed class ViewingItemClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private GameObject _ui;

        public bool IsEnabled
        {
            get
            {
                if (_ui == null) return false;
                return _ui.activeSelf;
            }
            set
            {
                if (_ui == null) return;
                _ui.SetActive(value);
            }
        }
    }
}