using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class RotateDoor : AMovableDoor
    {
        [SerializeField, Tooltip("回転角度")]
        private Vector3 _angle;

        public async override UniTask PlayDoorOnce(CancellationToken ct)
        {
            await base.PlayDoorOnce(ct);

            await _doorTf
                .DOLocalRotate(_angle, _duration)
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
        }
    }
}