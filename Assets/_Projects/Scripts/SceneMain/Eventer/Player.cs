using Cysharp.Threading.Tasks;
using StarterAssets;
using System;
using System.Threading;
using UnityEngine;
using Main.EventManager;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Player
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private Camera _camera;

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

        public bool IsMoving => Mathf.Abs(_characterController.velocity.magnitude) > 0.01f;

        private bool _isGrounded = true;
        private bool _isPreGrounded = true;
        private bool _isBecameGrounded = false;
        public bool IsBecameGrounded => _isBecameGrounded;

        // 最初に呼んでほしい
        public async UniTaskVoid StartUpdatingGroundedFlag(CancellationToken ct)
        {
            while (true)
            {
                _isGrounded = _characterController.isGrounded;
                _isBecameGrounded = _isGrounded && !_isPreGrounded;
                _isPreGrounded = _isGrounded;

                await UniTask.NextFrame(ct);
            }
        }

        // 委譲するだけ
        public float SlopLimit { set => _firstPersonController.SlopeLimit = value; }

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
        /// プレイヤーが不正な場所にいったら、強制敵に初期座標に戻す
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
    }
}