using System;
using Sirenix.OdinInspector;
using UnityEngine;
using BorderSystem;

namespace Main.Eventer.Borders
{
    // Inに入る → FirstTfにテレポート → Outに入る → SecondTfにテレポート
    [Serializable]
    public sealed class TeleportBorder
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Border _in;
        public Border In => _in;

        [SerializeField, Required, SceneObjectsOnly]
        private Border _out;
        public Border Out => _out;

        [SerializeField, Required, SceneObjectsOnly]
        private Transform _firstTf;
        public Transform FirstTf => _firstTf;

        [SerializeField, Required, SceneObjectsOnly]
        private Transform _secondTf;
        public Transform SecondTf => _secondTf;
    }
}