
using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Main.Eventer.PlayerChasingCharacter
{
    public abstract class APlayerChasingCharacter
    {
        protected sealed class MonoBehaviourComponent : MonoBehaviour { }

        [SerializeField, Required, SceneObjectsOnly]
        protected Transform _playerTransform;

        [SerializeField, Required, SceneObjectsOnly]
        protected NavMeshAgent _navMeshAgent;

        protected virtual float InitSpeed => 3.5f;
        protected virtual float InitAngularSpeed => 120.0f;
        protected virtual float InitAcceleration => 8.0f;

        protected bool _isInitialized = false;

        protected bool _isEnabled = false;
        protected bool isEnabled
        {
            get => _isEnabled;
            set
            {
                if (_navMeshAgent == null) return;

                _isEnabled = value;
                _navMeshAgent.gameObject.SetActive(_isEnabled);

                if (value is true && _isInitialized is false)
                {
                    _isInitialized = true;

                    MonoBehaviourComponent monoBehaviourComponent = _navMeshAgent.gameObject.AddComponent<MonoBehaviourComponent>();

                    if (monoBehaviourComponent != null)
                    {
                        monoBehaviourComponent.GetAsyncUpdateTrigger().
                        Subscribe(_ => ChasePlayerOnUpdateIfAvailable(_playerTransform)).
                        AddTo(monoBehaviourComponent.destroyCancellationToken);
                    }
                }
            }
        }

        private void ChasePlayerOnUpdateIfAvailable(Transform playerTransform)
        {
            if (_isEnabled is false) return;
            if (playerTransform == null) return;
            if (_navMeshAgent == null) return;
            if (_navMeshAgent.isOnNavMesh is false) return;
            ChasePlayerOnUpdateIfAvailableWithoutNullCheck(playerTransform);
        }

        protected virtual void ChasePlayerOnUpdateIfAvailableWithoutNullCheck(Transform playerTransform)
        {
            _navMeshAgent.SetDestination(playerTransform.position);
            _navMeshAgent.transform.LookAt(playerTransform);
        }

        protected virtual void OnSpawn() { }
        protected virtual void OnDespawn() { }

        public void InitNavMeshAgent()
        {
            Speed = InitSpeed;
            AngularSpeed = InitAngularSpeed;
            Acceleration = InitAcceleration;
        }

        // 既にアクティブの場合、テレポートと同義
        public void SpawnHere(Transform transform)
        {
            if (_navMeshAgent == null) return;
            if (transform == null) return;
            _navMeshAgent.Warp(transform.position);
            _navMeshAgent.transform.rotation = transform.rotation;
            isEnabled = true;
            OnSpawn();
        }

        public void Despawn()
        {
            if (_navMeshAgent == null) return;
            isEnabled = false;
            OnDespawn();
        }

        public float Speed
        {
            get
            {
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.speed;
            }
            set
            {
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 50.0f)) return;
                _navMeshAgent.speed = value;
            }
        }

        public float AngularSpeed
        {
            get
            {
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.angularSpeed;
            }
            set
            {
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 359.9f)) return;
                _navMeshAgent.angularSpeed = value;
            }
        }

        public float Acceleration
        {
            get
            {
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.acceleration;
            }
            set
            {
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 100.0f)) return;
                _navMeshAgent.acceleration = value;
            }
        }
    }
}
