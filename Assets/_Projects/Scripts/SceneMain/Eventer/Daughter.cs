using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Daughter
    {
        [SerializeField, Required, SceneObjectsOnly]
        private NavMeshAgent _navMeshAgent;

        [SerializeField, Header("ナイフ(小道に散らばる予定)")]
        private GameObject _knife;

        private static readonly float InitSpeed = 3.5f;
        private static readonly float InitAngularSpeed = 120.0f;
        private static readonly float InitAcceleration = 8.0f;
        private static readonly float DistanceFromPlayer = 2;

        public bool IsActive
        {
            get
            {
                if (_navMeshAgent == null) return false;
                return _navMeshAgent.gameObject.activeSelf;
            }
            set
            {
                if (_navMeshAgent == null) return;
                _navMeshAgent.gameObject.SetActive(value);
            }
        }

        public float Speed
        {
            get
            {
                if (IsActive is false) return 0;
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.speed;
            }
            set
            {
                if (IsActive is false) return;
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 50.0f)) return;
                _navMeshAgent.speed = value;
            }
        }

        public float AngularSpeed
        {
            get
            {
                if (IsActive is false) return 0;
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.angularSpeed;
            }
            set
            {
                if (IsActive is false) return;
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 359.9f)) return;
                _navMeshAgent.angularSpeed = value;
            }
        }

        public float Acceleration
        {
            get
            {
                if (IsActive is false) return 0;
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.acceleration;
            }
            set
            {
                if (IsActive is false) return;
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 100.0f)) return;
                _navMeshAgent.acceleration = value;
            }
        }

        public void InitNavMeshAgent()
        {
            Speed = InitSpeed;
            AngularSpeed = InitAngularSpeed;
            Acceleration = InitAcceleration;
        }

        // 引数はワールド座標
        public void ReTargetThisPlayer(Vector3 playerPosition)
        {
            if (IsActive is false) return;
            if (_navMeshAgent == null) return;

            Vector3 daughterToPlayer = playerPosition - _navMeshAgent.transform.position;
            if (daughterToPlayer.sqrMagnitude <= DistanceFromPlayer * DistanceFromPlayer) return;
            Vector3 targetPosition = playerPosition - daughterToPlayer.normalized * DistanceFromPlayer;
            _navMeshAgent.SetDestination(targetPosition);
        }

        // 一括変更
        public void SetPathWayItemsEnabled(bool value)
        {
            if (_knife != null) _knife.SetActive(value);
        }

        // 個別変更(一括変更の方と競合し得る)
        public void SetKnifeEnabled(bool value)
        {
            if (_knife != null) _knife.SetActive(value);
        }
    }
}