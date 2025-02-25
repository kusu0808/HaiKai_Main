using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Death
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Animator _deathMovieAnimator;

        [SerializeField, Required, SceneObjectsOnly]
        private AudioSource _deathMovieAudioSource;

        [SerializeField, Required, AssetsOnly]
        private AudioClip _deathMovieAudioClip;

        [SerializeField, Required, SceneObjectsOnly]
        private GameObject _goToTitleUI;

        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _goToTitleText;

        private void OnEnable() => Main(destroyCancellationToken).Forget();

        private async UniTaskVoid Main(CancellationToken ct)
        {
            await UniTask.DelayFrame(2, cancellationToken: ct);
            if (_deathMovieAnimator != null) _deathMovieAnimator.SetTrigger("PlayOnce");
            if (_deathMovieAudioSource != null) _deathMovieAudioSource.Raise(_deathMovieAudioClip, SoundType.SE);
            await UniTask.WaitForSeconds(2.0f, cancellationToken: ct);

            if (_goToTitleUI != null) _goToTitleUI.SetActive(true);
            for (int i = 5; i >= 0; i--)
            {
                if (i <= 0) Scene.ID.Title.LoadAsync().Forget();
                if (_goToTitleText != null) _goToTitleText.text = $"Game Over\n<size=72> </size>\n<size=108>({i}秒後にタイトルに戻ります...)</size>";
                await UniTask.WaitForSeconds(1, cancellationToken: ct);
            }
        }
    }
}