
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

        protected override float InitSpeed => 1.0f;

        public bool IsSteppingOnGlassShard { get; set; } = false;
        public AudioClip ChasedBGM { get; set; } = null;
        private CancellationTokenSource _chasedBGMCts = null;

        // 使い回すので、特別に生成
        private AudioSource _audioSource = null;

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
            base.OnSpawn();
            if (_animator != null) _animator.SetBool("IsMoving", true);
        }

        protected override void OnDespawn()
        {
            base.OnDespawn();
            if (_animator != null) _animator.SetBool("IsMoving", false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_navMeshAgent != null) _audioSource = _navMeshAgent.gameObject.AddComponent<AudioSource>();

            _chasedBGMCts = new CancellationTokenSource();
            StartObservingChasedBGM(_chasedBGMCts.Token).Forget();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _chasedBGMCts?.Cancel();
            _chasedBGMCts?.Dispose();
            _chasedBGMCts = null;

            UnityEngine.Object.Destroy(_audioSource);
            _audioSource = null;
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
