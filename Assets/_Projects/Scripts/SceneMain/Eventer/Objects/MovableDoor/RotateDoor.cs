using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class RotateDoor : AMovableDoor
    {
        [SerializeField, Tooltip("回転角度")]
        private Vector3 _angle;

        protected override async UniTaskVoid DoMoveWithoutNullCheck(Transform transform, CancellationToken ct) =>
            await transform
                .DOLocalRotate(_angle, _duration)
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
    }
}