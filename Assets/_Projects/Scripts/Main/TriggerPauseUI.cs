using Cysharp.Threading.Tasks;
using General;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    /// <summary>
    /// ゲームの一時停止、ポーズUIの表示切替
    /// </summary>
    public sealed class TriggerPauseUI : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseUICanvas;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _toTitleButton;
        [SerializeField] private TriggerSettingUI _triggerSettingUI;

        private GameObject _pauseUI => _pauseUICanvas.gameObject;
        private bool _isPauseTrigger => IA.InputGetter.Instance.Pause.Bool && !_triggerSettingUI.IsActive;

        private void OnEnable()
        {
            _pauseUI.SetActive(false);

            _settingButton.onClick.AddListener(_triggerSettingUI.Open);
            _toTitleButton.onClick.AddListener(LoadSceneAsync);

            Trigger(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask Trigger(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => _isPauseTrigger, cancellationToken: ct);
                _pauseUI.SetActive(!_pauseUI.activeSelf);
                Time.timeScale = _pauseUI.activeSelf ? 0 : 1;
            }
        }

        /// <summary>
        /// 後方互換
        /// </summary>
        private void LoadSceneAsync() => UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}