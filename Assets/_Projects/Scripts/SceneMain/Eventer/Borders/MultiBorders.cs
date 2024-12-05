using System;
using Sirenix.OdinInspector;
using UnityEngine;
using BorderSystem;

namespace Main.Eventer.Borders
{
    // いずれかの中に入っているか
    [Serializable]
    public sealed class MultiBorders
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Border[] _elements;

        public bool IsInAny(Vector3 pos)
        {
            if (_elements is null) return false;
            foreach (Border border in _elements)
            {
                if (border == null) continue;
                if (border.IsIn(pos) is true) return true;
            }
            return false;
        }
    }
}