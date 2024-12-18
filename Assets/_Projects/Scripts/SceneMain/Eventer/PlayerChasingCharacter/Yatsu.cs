
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.PlayerChasingCharacter
{
    [Serializable]
    public sealed class Yatsu : APlayerChasingCharacter
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Animator _animator;
        [SerializeField, Required, SceneObjectsOnly]
        private Transform _transform;

        protected override float InitSpeed => 0.5f;

        public bool IsSteppingOnGlassShard { get; set; } = false;

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

        protected override void ChasePlayerOnUpdateIfAvailableWithoutNullCheck(Transform playerTransform)
        {
            if (IsSteppingOnGlassShard is true) return;

            _navMeshAgent.SetDestination(playerTransform.position);
            _navMeshAgent.transform.LookAt(playerTransform);
        }

        protected override void OnSpawn()
        {
            if (_animator == null) return;
            _animator.SetBool("IsMoving", true);
        }

        protected override void OnDespawn()
        {
            if (_animator == null) return;
            _animator.SetBool("IsMoving", false);
        }

        public bool IsMoving => Mathf.Abs(Speed) > 0.01f;
    }
}
