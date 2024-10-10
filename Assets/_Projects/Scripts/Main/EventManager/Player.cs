using Cysharp.Threading.Tasks;
using StarterAssets;
using System;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    [Serializable]
    public sealed class Player
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private Camera _camera;
        [SerializeField, Range(0.1f, 10.0f), Tooltip("アクションRayの最大距離")] private float _actionRayMaxDistance;

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

        /// <summary>
        /// カメラの正面方向にRayを飛ばし、当たったColliderを返す
        /// </summary>
        public Collider GetHitColliderFromCamera()
        {
            Ray ray = _camera.ScreenPointToRay(new(Screen.width / 2, Screen.height / 2, 0));
            return Physics.Raycast(ray, out RaycastHit hitInfo, _actionRayMaxDistance) ? hitInfo.collider : null;
        }

        /// <summary>
        /// プレイヤーの移動と回転を0にする
        /// </summary>
        public void StopPlayerMove()
        {
            _firstPersonController.MoveSpeed = 0;
            _firstPersonController.SprintSpeed = 0;
            _firstPersonController.RotationSpeed = 0;
        }

        /// <summary>
        /// プレイヤーの移動と回転を初期に戻す
        /// </summary>
        public void InitPlayerMove()
        {
            _firstPersonController.MoveSpeed = 4;
            _firstPersonController.SprintSpeed = 6;
            _firstPersonController.RotationSpeed = 2;
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