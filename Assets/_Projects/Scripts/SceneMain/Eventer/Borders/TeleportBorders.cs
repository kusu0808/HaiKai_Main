using System;
using Sirenix.OdinInspector;
using UnityEngine;
using BorderSystem;

namespace Main.Eventer.Borders
{
    // どれかのInに入る → 対応するOutTfにテレポート → どれかのOutに入る → 対応するInTfにテレポート
    [Serializable]
    public sealed class TeleportBorders
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Element[] _elements;

        /// <param name="layers">この中に入っていないものに関しては、判定を行わない(無視する)。空の場合は、レイヤーを考慮しないでメソッドの処理を行う</param>
        /// <returns>最初に見つけたもののインスタンスを返し、それ以外はnullを返す。</returns>
        public Element IsInAny(Vector3 pos, bool isBorderInType, params int[] layers)
        {
            if (_elements is null) return null;
            if (_elements.Length <= 0) return null;
            if (layers is null) return null;

            foreach (Element element in _elements)
            {
                if (element is null) continue;
                Border border = element.GetBorder(isBorderInType);
                if (border == null) continue;

                if (layers.Length > 0)
                {
                    foreach (int layer in layers)
                        if (border.IsIn(pos, layer) is true) return element;
                }
                else
                {
                    if (border.IsIn(pos) is true) return element;
                }
            }

            return null;
        }

        [Serializable]
        public sealed class Element
        {
            [SerializeField, Required, SceneObjectsOnly]
            private Border _in;

            [SerializeField, Required, SceneObjectsOnly]
            private Border _out;

            [SerializeField, Required, SceneObjectsOnly]
            private Transform _inTf;

            [SerializeField, Required, SceneObjectsOnly]
            private Transform _outTf;

            public Border GetBorder(bool isBorderInType) => isBorderInType ? _in : _out;
            public Transform GetTransform(bool isBorderInType) => isBorderInType ? _outTf : _inTf;
        }
    }
}