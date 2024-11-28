using System;
using Sirenix.OdinInspector;
using UnityEngine;
using General;

namespace Main.Eventer.PlayerChasingCharacter
{
    [Serializable]
    public sealed class Daughter : APlayerChasingCharacter
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("ナイフ(小道に散らばる予定)")]
        private GameObject _knife;

        private static readonly float DistanceFromPlayer = 2;

        protected override void RetargetPlayerWithoutNullCheck(Transform playerTransform)
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