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

        [SerializeField, Required, SceneObjectsOnly, Tooltip("ナイフ(小道に散らばる予定)")]
        private GameObject _knife;

        private enum MoveState
        {
            Idle,
            Moving,
            TiredOnMoving
        }

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

        private void UpdateAnimation()
        {
            if (_animator == null) return;

            switch (_moveState)
            {
                case MoveState.Idle:
                    {
                        if (GetIsMoving() is true)
                        {
                            _moveState = MoveState.Moving;
                            break;
                        }
                    }
                    break;
                case MoveState.Moving:
                    {
                        if (GetIsMoving() is false)
                        {
                            _movingTime = 0;
                            _moveState = MoveState.Idle;
                            break;
                        }

                        _movingTime += Time.deltaTime;
                        if (_movingTime >= TimeUntilBecomeTired)
                        {
                            _movingTime = 0;
                            _moveState = MoveState.TiredOnMoving;
                            _cachedSpeed = Speed;
                            Speed *= 0.5f;
                            break;
                        }
                    }
                    break;
                case MoveState.TiredOnMoving:
                    {
                        if (GetIsMoving() is false)
                        {
                            _moveState = MoveState.Idle;
                            Speed = _cachedSpeed;
                            _cachedSpeed = 0;
                            break;
                        }
                    }
                    break;
            }

            UpdateAnimatorParams();

            bool GetIsMoving()
            {
                if (_navMeshAgent == null) return false;
                return _navMeshAgent.velocity.sqrMagnitude > 0.01f;
            }
        }

        private void UpdateAnimatorParams()
        {
            if (_animator == null) return;

            _animator.SetInteger("MoveState", _moveState switch
            {
                MoveState.Idle => 0,
                MoveState.Moving => 1,
                MoveState.TiredOnMoving => 2,
                _ => 0
            });
        }

        public bool IsKnifeEnabled
        {
            get
            {
                if (_knife == null) return false;
                return _knife.activeSelf;
            }
            set
            {
                if (_knife == null) return;
                _knife.SetActive(value);
            }
        }
    }
}