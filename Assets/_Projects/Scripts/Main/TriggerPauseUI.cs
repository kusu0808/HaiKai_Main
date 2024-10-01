using Cysharp.Threading.Tasks;
using IA;
using UnityEngine;

namespace Main
{
    /// <summary>
    /// ゲームの一時停止、ポーズUIの表示切替
    /// </summary>
    public sealed class TriggerPauseUI : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseUICanvas;

        private GameObject _pauseUI => _pauseUICanvas.gameObject;
        private bool _isPause => InputGetter.Instance.Pause.Bool;

        private async void Start()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            _pauseUI.SetActive(false);

            while (true)
            {
                await UniTask.WaitUntil(() => _isPause, cancellationToken: ct);
                _pauseUI.SetActive(!_pauseUI.activeSelf);
                Time.timeScale = _pauseUI.activeSelf ? 0 : 1;
            }
        }

        private void OnDisable() => _pauseUICanvas = null;
    }
}