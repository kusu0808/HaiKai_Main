
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using General;
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
        private AudioSource _audioSource;

        protected override float InitSpeed => 0.1f;

        public bool IsSteppingOnGlassShard { get; set; } = false;
        public AudioClip ChasedBGM { get; set; } = null;

        public Vector3 Position
        {
            get
            {
                if (_navMeshAgent == null) return Vector3.zero;
                return _navMeshAgent.transform.position;
            }
        }

        public bool IsSlow { set { Speed = value ? 0 : InitSpeed; } }
        public bool IsFast { set { Speed = value ? InitSpeed * 3 : InitSpeed; } }

        protected override void ChasePlayerOnUpdateIfAvailableWithoutNullCheck(Transform playerTransform)
        {
            if (IsSteppingOnGlassShard is true) return;

            _navMeshAgent.SetDestination(playerTransform.position);
            _navMeshAgent.transform.LookAt(playerTransform);
        }

        protected override void OnSpawn()
        {
            if (_animator != null) _animator.SetBool("IsMoving", true);
        }

        protected override void OnDespawn()
        {
            if (_animator != null) _animator.SetBool("IsMoving", false);
        }

        protected override void OnEnable()
        {
            StartObservingChasedBGM(_navMeshAgent.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid StartObservingChasedBGM(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => isEnabled is true, cancellationToken: ct);
                if (_audioSource != null && ChasedBGM != null) _audioSource.Raise(ChasedBGM, SoundType.BGM);
                await UniTask.WaitUntil(() => isEnabled is false, cancellationToken: ct);
                if (_audioSource != null) _audioSource.Stop();
            }
        }
    }
}
