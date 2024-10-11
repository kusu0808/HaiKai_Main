using BorderSystem;
using System;
using UnityEngine;

namespace Main.EventManager
{
    [Serializable]
    public sealed class Borders
    {
        [SerializeField, Tooltip("仮変数")]
        private Border _borderHoge;
        public Border BorderHoge => _borderHoge;
    }
}