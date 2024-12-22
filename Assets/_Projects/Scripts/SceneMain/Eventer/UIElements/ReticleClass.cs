using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Eventer.UIElements
{
    [Serializable]
    public sealed class ReticleClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Image _reticleImage;

        [SerializeField, Required, AssetsOnly]
        private Sprite _normalSprite;

        [SerializeField, Required, AssetsOnly]
        private Sprite _clickSprite;

        public enum Type
        {
            Normal,
            Click
        }

        public void SetType(Type type)
        {
            Sprite newSprite = type switch
            {
                Type.Normal => _normalSprite,
                Type.Click => _clickSprite,
                _ => null
            };
            if (newSprite == null) return;

            if (_reticleImage == null) return;
            _reticleImage.sprite = newSprite;
        }

        public void ChangeColor(bool red)
        {
            if (_reticleImage == null) return;
            _reticleImage.color = red ? Color.red : Color.white;
        }
    }
}