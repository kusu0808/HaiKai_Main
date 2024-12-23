using System;
using Sirenix.OdinInspector;
using UnityEngine;
using General;

namespace Main.Eventer.PlayerChasingCharacter
{
    [Serializable]
    public sealed class Daughter : APlayerChasingCharacter
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Animator _animator;

        private enum AnimationMode
        {
            Enter,
            Escape
        }

        private enum MoveState
        {
            Idle,
            Walk,
            WalkTiredly // 逃走時には、この状態にはならない
        }

        private AnimationMode _animationMode = AnimationMode.Enter;
        private MoveState _moveState = MoveState.Idle;
        private float _movingTime = 0;
        private float _cachedSpeed = 0; // 足が遅くなる前のスピードを、一時的に保存

        private static readonly float DistanceFromPlayer = 2;
        private static readonly float TimeUntilBecomeTired = 5;

        protected override void ChasePlayerOnUpdateIfAvailableWithoutNullCheck(Transform playerTransform)
        {
            Vector2 daughterPos = _navMeshAgent.transform.position.WithoutY(out float daughterY);
            Vector2 playerPos = playerTransform.position.WithoutY(out _);
            Vector2 daughterToPlayer = playerPos - daughterPos;
            if (daughterToPlayer.sqrMagnitude > DistanceFromPlayer * DistanceFromPlayer)
            {
                Vector3 targetPos = (playerPos - daughterToPlayer.normalized * DistanceFromPlayer).WithY(daughterY);
                _navMeshAgent.SetDestination(targetPos);
            }

            _navMeshAgent.transform.LookAt(playerPos.WithY(daughterY));

            UpdateAnimation();
        }

        protected override void OnDespawn()
        {
            if (_animator == null) return;
            _moveState = MoveState.Idle;
            UpdateAnimatorParams();
        }

        /// <summary>
        /// 常にMoveStateを更新し、その後パラメーターを更新する
        /// </summary>
        private void UpdateAnimation()
        {
            if (_animator == null) return;

            Action action = (_moveState, _animationMode) switch
            {
                (MoveState.Idle, _) => DoIdle,
                (MoveState.Walk, AnimationMode.Enter) => DoWalkOnEnter,
                (MoveState.Walk, AnimationMode.Escape) => DoWalkOnEscape,
                (MoveState.WalkTiredly, _) => DoWalkTiredly,
                _ => null
            };

            action?.Invoke();
            UpdateAnimatorParams();



            void DoIdle()
            {
                if (GetIsMoving() is false) return;
                _moveState = MoveState.Walk;
            }

            void DoWalkOnEnter()
            {
                if (GetIsMoving() is false)
                {
                    _movingTime = 0;
                    _moveState = MoveState.Idle;
                }

                _movingTime += Time.deltaTime;
                if (_movingTime >= TimeUntilBecomeTired)
                {
                    _movingTime = 0;
                    _moveState = MoveState.WalkTiredly;
                    _cachedSpeed = Speed;
                    Speed *= 0.5f;
                }
            }

            void DoWalkOnEscape()
            {
                if (GetIsMoving() is true) return;
                _moveState = MoveState.Idle;
            }

            void DoWalkTiredly()
            {
                if (GetIsMoving() is true) return;
                _moveState = MoveState.Idle;
                Speed = _cachedSpeed;
                _cachedSpeed = 0;
            }

            bool GetIsMoving()
            {
                if (_navMeshAgent == null) return false;
                return _navMeshAgent.velocity.sqrMagnitude > 0.01f;
            }
        }

        /// <summary>
        /// アニメーションのステートを切り替える
        /// MoveStateを元に、パラメーターを更新する
        /// </summary>
        private void UpdateAnimatorParams()
        {
            if (_animator == null) return;

            _animator.SetInteger("MoveState", _moveState switch
            {
                MoveState.Idle => 0,
                MoveState.Walk => 1,
                MoveState.WalkTiredly => 2,
                _ => 0
            });
        }

        /// <summary>
        /// アニメーションのサブステートマシンを切り替える(入山時→逃走時)
        /// AnimationModeとパラメーターを同時に更新する
        /// </summary>
        public void ChangeAnimationModeFromEnterToEscape()
        {
            if (_animator == null) return;
            if (_animationMode is not AnimationMode.Enter) return;

            _animationMode = AnimationMode.Escape;
            _animator.SetTrigger("DoEndEnter");
            _animator.SetTrigger("DoStartEscape");
        }
    }
}