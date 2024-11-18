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

    public static class DaughterEx
    {
        public static Vector2 WithoutY(this Vector3 v, out float ignoredY)
        {
            ignoredY = v.y;
            return new(v.x, v.z);
        }

        public static Vector3 WithY(this Vector2 v, float y) => new(v.x, y, v.y);
    }
}