
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

        protected override float InitSpeed => 0.5f;

        public bool IsSteppingOnGlassShard { get; set; } = false;

        public Vector3 Position
        {
            get
            {
                if (_navMeshAgent == null) return Vector3.zero;
                return _navMeshAgent.transform.position;
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
    }
}
