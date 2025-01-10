using System;
using Sirenix.OdinInspector;
using UnityEngine;
using BorderSystem;
using System.Collections.Generic;

namespace Main.Eventer.Borders
{
    // いずれかの中に入っているか
    [Serializable]
    public sealed class MultiBorders
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Border[] _elements;

        public static MultiBorders New(Border border)
        {
            return new MultiBorders
            {
                _elements = new Border[] { border }
            };
        }

        public static MultiBorders New(params MultiBorders[] multiBorders)
        {
            if (multiBorders is null) return null;

            List<Border> borders = new();

            foreach (MultiBorders mb in multiBorders)
            {
                if (mb is null) continue;
                if (mb._elements is null) continue;
                borders.AddRange(mb._elements);
            }

            return new MultiBorders
            {
                _elements = borders.ToArray()
            };
        }

        /// <param name="layers">この中に入っていないものに関しては、判定を行わない(無視する)。空の場合は、レイヤーを考慮しないでメソッドの処理を行う</param>
        public bool IsInAny(Vector3 pos, params int[] layers)
        {
            if (_elements is null) return false;
            if (_elements.Length <= 0) return false;
            if (layers is null) return false;

            foreach (Border border in _elements)
            {
                if (border == null) continue;

                if (layers.Length > 0)
                {
                    foreach (int layer in layers)
                        if (border.IsIn(pos, layer) is true) return true;
                }
                else
                {
                    if (border.IsIn(pos) is true) return true;
                }
            }

            return false;
        }
    }
}