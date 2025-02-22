using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using General;
using Sirenix.OdinInspector;
using SO;
using UnityEngine;

namespace Title
{
    public sealed class TitleBGM : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly]
        private AudioSource _audioSource;

        private void OnEnable() => Play(destroyCancellationToken).Forget();

        private async UniTaskVoid Play(CancellationToken ct)
        {
            _audioSource.volume = 0;
            _audioSource.Raise(SAudioClips.Entity.BGM.Title, SoundType.BGM);
            await UniTask.WaitForSeconds(1, cancellationToken: ct);
            await _audioSource.DOFade(1, 5).WithCancellation(ct);
        }
    }
}