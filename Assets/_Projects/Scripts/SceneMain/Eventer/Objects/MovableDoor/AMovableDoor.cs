using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    public abstract class AMovableDoor<T> where T : AMovableDoor<T>
    {
        private static readonly string ClassName = typeof(T).Name;
        [ReadOnly, ShowInInspector, LabelText("Class Name")]
        private string _ => ClassName;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("コライダー")]
        private Collider _collider;

        [SerializeField, Required, Tooltip("開く時の、ローカル変化量")]
        private Vector3 _delta;

        [SerializeField, Required, Tooltip("補間方法")]
        protected Ease _ease;

        [SerializeField, Range(0.1f, 20.0f), Tooltip("アニメーション時間")]
        protected float _duration;

        [SerializeField, Tooltip("trueなら1回限り、falseなら何回も動く")]
        private bool _isMoveOnce;

        private bool _hasPlayedIfMoveOnce = false;
        private bool _hasOpenedIfNotMoveOnce = false;

        public void Trigger()
        {
            if (_collider == null) return;

            if (_isMoveOnce)
            {
                if (_hasPlayedIfMoveOnce is true) return;
                UpdateColliderAndDoMove(true);
                _hasPlayedIfMoveOnce = true;
            }
            else
            {
                UpdateColliderAndDoMove(!_hasOpenedIfNotMoveOnce);
                Inverse(ref _hasOpenedIfNotMoveOnce);
            }

            static void Inverse(ref bool value) => value = !value;
            void UpdateColliderAndDoMove(bool isOpen)
            {
                _collider.enabled = !isOpen; // 当たり判定とRayCast判定が同時に有効/無効化

                DoMove(
                    _collider.transform,
                    isOpen ? _delta : -_delta,
                    _collider.GetCancellationTokenOnDestroy()
                    ).Forget();
            }
        }

        protected abstract UniTaskVoid DoMove(Transform transform, Vector3 delta, CancellationToken ct);
    }
}
