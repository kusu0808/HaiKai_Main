using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.MovableDoor
{
    public abstract class AMovableDoor
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("コライダー")]
        private Collider _collider;

        [SerializeField, Required, Tooltip("補間方法")]
        protected Ease _ease;

        [SerializeField, Range(0.1f, 20.0f), Tooltip("アニメーション時間")]
        protected float _duration;

        private bool _hasPlayed = false;

        /// <summary>
        /// 一回限り
        /// </summary>
        public void Open()
        {
            if (_collider == null) return;
            if (_hasPlayed is true) return;
            _hasPlayed = true;
            _collider.enabled = false; // 当たり判定とRayCast判定が同時に無効化される
            DoMoveWithoutNullCheck(_collider.transform, _collider.GetCancellationTokenOnDestroy()).Forget();
        }

        protected abstract UniTaskVoid DoMoveWithoutNullCheck(Transform transform, CancellationToken ct);
    }
}
