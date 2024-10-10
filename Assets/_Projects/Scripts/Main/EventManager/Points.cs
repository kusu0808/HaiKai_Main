using System;
using UnityEngine;

namespace Main.EventManager
{
    [Serializable]
    public sealed class Points
    {
        [SerializeField, Tooltip("落下などで強制引き戻しする場合も、ここに戻す")]
        private Transform _init;
        public Transform Init => _init;
    }
}