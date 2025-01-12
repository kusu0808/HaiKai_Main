using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace Death
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly]
        private VideoPlayer _videoPlayer;

        [SerializeField, Required, AssetsOnly]
        private RenderTexture _renderTexture;

        [SerializeField, Required, SceneObjectsOnly]
        private GameObject _goToTitleUI;

        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _goToTitleText;

        private async UniTaskVoid OnEnable()
        {
            if (_videoPlayer == null) return;
            if (_renderTexture == null) return;

            _videoPlayer.loopPointReached += _ => OnVideoEnd(destroyCancellationToken).Forget();
            _renderTexture.Release();
            _videoPlayer.Play();
        }

        private async UniTaskVoid OnVideoEnd(CancellationToken ct)
        {
            await UniTask.WaitForSeconds(1, cancellationToken: ct);

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