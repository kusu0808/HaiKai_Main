using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Main.Eventer.UIElements
{
    [Serializable]
    public sealed class LabelClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _labelText;

        public bool IsEnabled
        {
            get
            {
                if (_labelText == null) return false;
                return _labelText.enabled;
            }
            set
            {
                if (_labelText == null) return;
                _labelText.enabled = value;
            }
        }
    }
}