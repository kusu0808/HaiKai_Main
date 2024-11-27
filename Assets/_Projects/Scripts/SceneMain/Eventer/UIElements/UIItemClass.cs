using UnityEngine;
using Sirenix.OdinInspector;

namespace Main.Eventer.UIElements
{
    public sealed class UIItemClass
    {
        [SerializeField, Required, AssetsOnly]
        private Sprite _sprite;

        private ManagePlayerUI _managePlayerUI;
        private int _index; // アイテムの入手順が決まっているので、それぞれのアイテム毎に、何番目に入れるかを厳密に指定する

        // 最初に呼ぶこと！
        public void Init(ManagePlayerUI managePlayerUI, int initIndex)
        {
            _managePlayerUI = managePlayerUI;
            _index = initIndex;
        }

        private bool IsHoldingThisIndex(int index) => (_managePlayerUI == null) ? false : index == _managePlayerUI.ItemIndex;

        private bool _isShow = false;
        public bool IsShow
        {
            get => _isShow;
            set
            {
                if (_managePlayerUI == null) return;
                _managePlayerUI.SetSprite(_index, value ? _sprite : null);
                _isShow = value;
            }
        }

        public int Index
        {
            set
            {
                if (_managePlayerUI == null) return;
                _managePlayerUI.SetSprite(_index, null);
                _index = value;
                _managePlayerUI.SetSprite(_index, _sprite);
            }
        }
        public bool IsHolding() => IsShow && IsHoldingThisIndex(_index);
    }
}