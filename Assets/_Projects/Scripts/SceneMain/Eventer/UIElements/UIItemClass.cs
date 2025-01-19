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

        private ManagePlayerUI _managePlayerUI = null;

        public void Init(ManagePlayerUI managePlayerUI) => _managePlayerUI = managePlayerUI;

        public bool IsHolding() => (_managePlayerUI == null) ? false : _managePlayerUI.IsHolding(_sprite);
        public void Obtain() { if (_managePlayerUI != null) _managePlayerUI.SetSprite(_sprite, true); }
        public void Release() { if (_managePlayerUI != null) _managePlayerUI.SetSprite(_sprite, false); }
    }
}