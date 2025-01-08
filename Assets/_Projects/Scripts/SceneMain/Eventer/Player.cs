using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using StarterAssets;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Main.EventManager;

using General;
using Sirenix.OdinInspector;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Player
    {
        [SerializeField, Required, SceneObjectsOnly] private Transform _transform;
        [SerializeField, Required, SceneObjectsOnly] private CharacterController _characterController;
        [SerializeField, Required, SceneObjectsOnly] private FirstPersonController _firstPersonController;
        [SerializeField, Required, SceneObjectsOnly] private Camera _camera;
        [SerializeField, Required, SceneObjectsOnly] private MeshRenderer _meshRenderer;

        public Vector3 Position
        {
            get
            {
                if (_transform == null) return Vector3.zero;
                return _transform.position;
            }
            set
            {
                if (_transform == null) return;
                _transform.position = value;
            }
        }

        public Vector3 LocalPosition
        {
            get
            {
                if (_transform == null) return Vector3.zero;
                return _transform.localPosition;
            }
            set
            {
                if (_transform == null) return;
                _transform.localPosition = value;
            }
        }

        public Vector3 EulerAngles
        {
            get
            {
                if (_transform == null) return Vector3.zero;
                return _transform.eulerAngles;
            }
            set
            {
                if (_transform == null) return;
                _transform.eulerAngles = value;
            }
        }

        public Vector3 LocalEulerAngles
        {
            get
            {
                if (_transform == null) return Vector3.zero;
                return _transform.localEulerAngles;
            }
            set
            {
                if (_transform == null) return;
                _transform.localEulerAngles = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                if (_meshRenderer == null) return false;
                return _meshRenderer.enabled;
            }
            set
            {
                if (_meshRenderer == null) return;
                _meshRenderer.enabled = value;
            }
        }

        public bool IsMoving => Mathf.Abs(_characterController.velocity.magnitude) > 0.01f;

        // 委譲するだけ
        public float SlopLimit { set => _firstPersonController.SlopeLimit = value; }

        // 最初に呼んで欲しい、委譲するだけ
        public void SubscribeGrounded(Action action) => _firstPersonController.GroundedSubject.Subscribe(_ => action?.Invoke()).AddTo(_firstPersonController);

        /// <summary>
        /// 完全にデバッグ用。戻すことはできない。
        /// </summary>
        public void FastenPlayer() => _firstPersonController.MoveSpeed *= 5;

        /// <summary>
        /// 完全にデバッグ用。戻すことはできない。
        /// </summary>
        public void FastenLook() => _firstPersonController.RotationSpeed *= 3;

        /// <summary>
        /// カメラの正面方向にRayを飛ばし、当たったColliderを返す
        /// 当たらなかったらnull
        /// </summary>
        public Collider GetHitColliderFromCamera()
        {
            Ray ray = _camera.ScreenPointToRay(new(Screen.width / 2, Screen.height / 2, 0));
            return Physics.Raycast(ray, out var hitInfo, EventManagerConst.RayMaxDistance) ? hitInfo.collider : null;
        }

        private bool _isPlayerControlEnabled = true;
        /// <summary>
        /// 移動・回転・ジャンプ
        /// </summary>
        public bool IsPlayerControlEnabled
        {
            get => _isPlayerControlEnabled;
            set
            {
                _isPlayerControlEnabled = value;
                _firstPersonController.IsMoveEnabled = value;
                _firstPersonController.IsLookEnabled = value;
                _firstPersonController.IsJumpEnabled = value;
            }
        }

        /// <summary>
        /// プレイヤーの座標と回転角を設定する
        /// </summary>
        /// <remarks>大きさは無視</remarks>
        public void SetTransform(Transform transform)
        {
            if (transform == null) return;

            Position = transform.position;
            EulerAngles = transform.eulerAngles;
        }

        /// <summary>
        /// 最初に呼んで欲しい、プレイヤーが不正な場所にいったら、強制敵に初期座標に戻す
        /// </summary>
        public async UniTaskVoid CheckDeviation(Transform initTransform, CancellationToken ct)
        {
            if (initTransform == null) return;

            while (true)
            {
                // 5秒ごとにチェックする
                await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: ct);

                // 落下のチェック(y=-20 が境界)
                if (Position.y < -20) SetTransform(initTransform);
            }
        }

        /// <summary>
        /// 最初に呼んで欲しい、ヤツとの接触を検知
        /// </summary>
        public void SubscribeYatsuCollision()
        {
            if (_characterController == null) return;

            _characterController.
                GetAsyncControllerColliderHitTrigger().
                Subscribe(OnControllerColliderHit).
                AddTo(_characterController.GetCancellationTokenOnDestroy());
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit == null) return;
            Collider collider = hit.collider;
            if (collider == null) return;
            if (collider.CompareTag("Character/Yatsu") is false) return;
            OnHitYatsu();
        }

        private void OnHitYatsu() => Scene.ID.Death.LoadAsync().Forget();
    }
}