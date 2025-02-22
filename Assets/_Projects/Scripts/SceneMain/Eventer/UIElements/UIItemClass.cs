using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Main.Eventer.UIElements
{
    [Serializable]
    public sealed class UIItemClass
    {
        [SerializeField, Required, AssetsOnly]
        private Sprite _sprite;

        private ManageItemUI _managePlayerUI = null;

        public void Init(ManageItemUI managePlayerUI) => _managePlayerUI = managePlayerUI;

        public bool IsHolding() => (_managePlayerUI == null) ? false : _managePlayerUI.IsHolding(_sprite);
        public bool HasItem() => (_managePlayerUI == null) ? false : _managePlayerUI.HasItem(_sprite);
        public void Obtain() { if (_managePlayerUI != null) _managePlayerUI.SetSprite(_sprite, true); }
        public void Release() { if (_managePlayerUI != null) _managePlayerUI.SetSprite(_sprite, false); }
    }
}