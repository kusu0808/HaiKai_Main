using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class DaughterChainClass
    {
        public enum Type
        {
            Chain1,
            Chain2,
            Chain3
        }

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _chain1;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _chain2;

        [SerializeField, Required, SceneObjectsOnly]
        private PlacedItemClass _chain3;

        public void Cut(Type type)
        {
            var chain = GetChain(type);
            if (chain == null) return;
            chain.IsEnabled = false;
        }

        public bool IsAllCut()
        {
            if (_chain1.IsEnabled) return false;
            if (_chain2.IsEnabled) return false;
            if (_chain3.IsEnabled) return false;
            return true;
        }

        private PlacedItemClass GetChain(Type type) => type switch
        {
            Type.Chain1 => _chain1,
            Type.Chain2 => _chain2,
            Type.Chain3 => _chain3,
            _ => null
        };
    }
}