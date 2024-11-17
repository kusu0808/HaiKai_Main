using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Daughter : ASerializedPlayerChasingCharacter
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("ナイフ(小道に散らばる予定)")]
        private GameObject _knife;

        private static readonly float DistanceFromPlayer = 2;

        protected override void ChasePlayerOnUpdateIfAvailable(Transform playerTransform)
        {
            if (_isEnabled is false) return;
            if (_navMeshAgent == null) return;
            if (playerTransform == null) return;
            if (_navMeshAgent.isOnNavMesh is false) return;

            Vector3 daughterToPlayer = playerTransform.position - _navMeshAgent.transform.position;
            if (daughterToPlayer.sqrMagnitude > DistanceFromPlayer * DistanceFromPlayer)
            {
                Vector3 targetPosition = playerTransform.position - daughterToPlayer.normalized * DistanceFromPlayer;
                _navMeshAgent.SetDestination(targetPosition);
            }

            _navMeshAgent.transform.LookAt(playerTransform);
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