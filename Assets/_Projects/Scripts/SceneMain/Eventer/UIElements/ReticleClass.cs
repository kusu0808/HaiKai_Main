using System;
using General;
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

        public static Color ColorNormal => Color.white;
        public static Color ColorActionAgainstCollider => Color.red;

        public Color Color
        {
            get
            {
                if (_reticleImage == null) return default;
                return _reticleImage.color;
            }
            set
            {
                if (_reticleImage == null) return;
                _reticleImage.color = value;
            }
        }

        public bool IsInvisible
        {
            get
            {
                if (_reticleImage == null) return default;
                return !_reticleImage.enabled;
            }
            set
            {
                if (_reticleImage == null) return;
                _reticleImage.enabled = !value;
            }
        }
    }
}