using System;
using Sirenix.OdinInspector;
using UnityEngine;
using BorderSystem;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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

        public static ReadOnlyCollection<Border> JoinAll(params MultiBorders[] multiBorders)
        {
            if (multiBorders is null) return null;
            if (multiBorders.Length <= 0) return null;

            List<Border> borders = new();
            foreach (MultiBorders mb in multiBorders)
            {
                if (mb is null) continue;
                if (mb._elements is null) continue;
                foreach (Border b in mb._elements)
                {
                    if (b == null) continue;
                    borders.Add(b);
                }
            }
            return borders.AsReadOnly();
        }
    }

    public static class ReadOnlyCollectionBorderEx
    {
        public static bool IsInAny(this ReadOnlyCollection<Border> borders, Vector3 pos)
        {
            if (borders is null) return false;
            foreach (Border border in borders)
            {
                if (border == null) continue;
                if (border.IsIn(pos) is true) return true;
            }
            return false;
        }
    }
}