using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class SlideDoor : AMovableDoor
    {
        [SerializeField, Range(0.1f, 100.0f), Tooltip("移動距離\nx座標のみを移動する")]
        private float _distance;

        protected override async UniTaskVoid DoMoveWithoutNullCheck(Transform transform, CancellationToken ct) =>
            await transform
                .DOLocalMoveX(_distance, _duration)
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
    }
}