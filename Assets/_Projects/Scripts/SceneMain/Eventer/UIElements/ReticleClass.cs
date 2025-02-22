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

        public static Color ColorNormal => Color.white;
        public static Color ColorActionAgainstCollider => Color.red;

        public static float SizeDefault => 1.0f;
        public static float SizeBig => 2.0f;

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

        public float Size
        {
            get
            {
                if (_reticleImage == null) return default;
                return _reticleImage.rectTransform.localScale.x; // yも同じはず
            }
            set
            {
                if (_reticleImage == null) return;
                _reticleImage.rectTransform.localScale = new Vector3(value, value, 1);
            }
        }
    }
}