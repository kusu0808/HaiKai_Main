using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class SlideDoor : AMovableDoor
    {
        [SerializeField, Tooltip("移動距離\nx座標のみを移動する")]
        private float _distance;

        public async override UniTask PlayDoorOnce(CancellationToken ct)
        {
            await base.PlayDoorOnce(ct);

            await _doorTf
                .DOLocalMoveX(_distance, _duration)
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
        }
    }
}