using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class DaughterChainClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _chain1;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _chain2;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _chain3;

        public void Cut1() => _chain1.IsEnabled = false;
        public void Cut2() => _chain2.IsEnabled = false;
        public void Cut3() => _chain3.IsEnabled = false;

        public bool IsAllCut()
        {
            if (_chain1.IsEnabled) return false;
            if (_chain2.IsEnabled) return false;
            if (_chain3.IsEnabled) return false;
            return true;
        }
    }
}