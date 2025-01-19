using Cysharp.Threading.Tasks;
using General;
using IA;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using SceneGeneral;
using UniRx;

namespace Main
{
    /// <summary>
    /// ゲームの一時停止、ポーズUIの表示切替
    /// 最初にTrigger()を呼ぶこと
    /// </summary>
    public sealed class TriggerPauseUI : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseUICanvas;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _toTitleButton;
        [SerializeField] private TriggerSettingUI _triggerSettingUI;

        private GameObject _pauseUI => _pauseUICanvas.gameObject;
        private bool _isPauseTrigger => InputGetter.Instance.Pause.Bool && !_triggerSettingUI.IsActive;

        public static Subject<Unit> OnPauseBegin { get; set; } = new Subject<Unit>();
        public static Subject<Unit> OnPauseEnd { get; set; } = new Subject<Unit>();

        private void OnEnable()
        {
            _pauseUI.SetActive(false);

            _settingButton.onClick.AddListener(_triggerSettingUI.Open);
            _toTitleButton.onClick.AddListener
                (() => Scene.ID.Title.LoadAsync().Forget());
        }

        /// <remarks>カーソルの状態も更新</remarks>
        public async UniTaskVoid Trigger(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => _isPauseTrigger, cancellationToken: ct);
                bool isPauseBegin = !_pauseUI.activeSelf;
                _pauseUI.SetActive(isPauseBegin);
                PauseState.IsPaused = isPauseBegin;
                SetCursor(isPauseBegin);

                if (isPauseBegin) OnPauseBegin?.OnNext(Unit.Default);
                else OnPauseEnd?.OnNext(Unit.Default);
            }
        }

        public void SetCursor(bool isActive)
        {
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}