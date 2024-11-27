using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Main.Eventer
{
    public sealed class DoorMover : MonoBehaviour
    {
        [SerializeField, Tooltip("ドアのTransform")]
        private Transform _doorTf;

        [SerializeField, Tooltip("補間方法")]
        private Ease _ease;

        [SerializeField, Tooltip("移動地点")]
        private float _point;

        [SerializeField, Tooltip("回転角度")]
        private Vector3 _angle;

        [SerializeField, Tooltip("アニメーション時間")]
        private float _duration;

        public async UniTask SlideDoor(CancellationToken ct)
        {
            if (_doorTf == null) return;

            await _doorTf
                .DOLocalMoveX(_point, _duration)
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
        }

        public async UniTask RotateDoor(CancellationToken ct)
        {
            if (_doorTf == null) return;

            await _doorTf
                .DOLocalRotate(_angle, _duration)
                .SetEase(_ease)
                .SetRelative()
                .ToUniTask(cancellationToken: ct);
        }
    }
}