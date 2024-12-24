using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class DaughterChainClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Collider[] _chains;

        public bool IsAllCut()
        {
            if (_chains is null) return false;
            if (_chains.Length <= 0) return false;

            foreach (Collider chain in _chains)
            {
                if (chain == null) continue;
                if (chain.gameObject.activeSelf) return false;
            }

            return true;
        }
    }
}