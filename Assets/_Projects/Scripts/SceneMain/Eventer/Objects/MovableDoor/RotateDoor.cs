using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class RotateDoor : AMovableDoor<RotateDoor>
    {
        protected override async UniTask DoMove(Transform transform, Vector3 delta, CancellationToken ct)
        {
            if (transform == null) return;
            await transform.DOLocalRotate(delta, _duration).SetEase(_ease).SetRelative().ToUniTask(cancellationToken: ct);
        }
    }
}