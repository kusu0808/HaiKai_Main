
using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Main
{
    [Serializable]
    public sealed class Yatsu
    {
        [SerializeField, Required, SceneObjectsOnly]
        private NavMeshAgent _navMeshAgent;

        [SerializeField, Required, SceneObjectsOnly]

        private bool _isInitialized = false;

        private bool _isEnabled = false;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                _navMeshAgent.gameObject.SetActive(_isEnabled);

                if (_isInitialized is false)
                {
                    _isInitialized = true;
                    YatsuChase yatsuChase = _navMeshAgent.AddComponent<YatsuChase>();
                    yatsuChase.GetAsyncUpdateTrigger().Subscribe(_ => ChasePlayerOnUpdateIfEnabled(Vector3.zero)).AddTo(yatsuChase.destroyCancellationToken);
                }
            }
        }

        // 引数はワールド座標
        private void ChasePlayerOnUpdateIfEnabled(Vector3 playerPosition)
        {
            _navMeshAgent.SetDestination(playerPosition);
        }

        public float Speed
        {
            get
            {
                if (IsEnabled is false) return 0;
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.speed;
            }
            set
            {
                if (IsEnabled is false) return;
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 50.0f)) return;
                _navMeshAgent.speed = value;
            }
        }

        public float AngularSpeed
        {
            get
            {
                if (IsEnabled is false) return 0;
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.angularSpeed;
            }
            set
            {
                if (IsEnabled is false) return;
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 359.9f)) return;
                _navMeshAgent.angularSpeed = value;
            }
        }

        public float Acceleration
        {
            get
            {
                if (IsEnabled is false) return 0;
                if (_navMeshAgent == null) return 0;
                return _navMeshAgent.acceleration;
            }
            set
            {
                if (IsEnabled is false) return;
                if (_navMeshAgent == null) return;
                if (value is not (> 0.1f and < 100.0f)) return;
                _navMeshAgent.acceleration = value;
            }
        }

        private sealed class YatsuChase : MonoBehaviour { }
    }
}
