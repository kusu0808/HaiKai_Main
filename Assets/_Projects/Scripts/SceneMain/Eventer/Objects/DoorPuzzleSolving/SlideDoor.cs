using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Main.Eventer.Objects.DoorPuzzleSolving
{
    [Serializable]
    public sealed class SlideDoor : AMovableDoor
    {
        [SerializeField, Range(-100.0f, 100.0f), Tooltip("移動距離\nx座標")]
        private float _distanceX;

        [SerializeField, Range(-100.0f, 100.0f), Tooltip("移動距離\nz座標")]
        private float _distanceZ;

        public override void Open() => Trigger();

        /// <summary>
        /// 開閉を交互に行う
        /// </summary>
        public void Trigger()
        {
            if (_collider == null) return;
            _hasPlayed = !_hasPlayed;
            _collider.enabled = !_hasPlayed; // 当たり判定とRayCast判定が同時に有効/無効化される
            DoMoveWithoutNullCheck(_collider.transform, _collider.GetCancellationTokenOnDestroy()).Forget();
        }

        protected override async UniTaskVoid DoMoveWithoutNullCheck(Transform transform, CancellationToken ct) =>
            await transform
                .DOLocalMove(new(_hasPlayed ? _distanceX : -_distanceX, transform.localPosition.y, _hasPlayed ? _distanceZ : -_distanceZ), _duration) // 開→閉→開→...
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
    }
}