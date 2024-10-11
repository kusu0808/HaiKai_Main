using Cysharp.Threading.Tasks;
using DG.Tweening;
using General;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.EventManager
{
    [Serializable]
    public sealed class UIElements
    {
        [SerializeField] private Image _blackImage;
        [SerializeField] private TextMeshProUGUI _logText;
        [Space(25)]
        [SerializeField] private ManagePlayerUI _managePlayerUI;
        [SerializeField] private TriggerPauseUI _triggerPauseUI;
        [SerializeField] private TriggerSettingUI _triggerSettingUI;

        private CancellationTokenSource _ctsLogText = new();

        /// <summary>
        /// [0, 1]
        /// </summary>
        public float BlackImageAlpha
        {
            get
            {
                if (_blackImage == null) return 0;
                return _blackImage.color.a;
            }
            set
            {
                if (_blackImage == null) return;
                Color color = _blackImage.color;
                color.a = value;
                _blackImage.color = color;
            }
        }

        /// <summary>
        /// 画面が暗くなっていないなら、フェードアウトする
        /// </summary>
        /// <remarks>並列に呼ばないこと</remarks>
        public async UniTask FadeOut(float duration, Ease ease, CancellationToken ct)
        {
            if (_blackImage == null) return;
            if (_blackImage.color.a != 0) return;

            await _blackImage.DOFade(1, duration).SetEase(ease).ToUniTask(cancellationToken: ct);
        }

        /// <summary>
        /// 画面が暗くなっているなら、フェードインする
        /// </summary>
        /// <remarks>並列に呼ばないこと</remarks>
        public async UniTask FadeIn(float duration, Ease ease, CancellationToken ct)
        {
            if (_blackImage == null) return;
            if (_blackImage.color.a != 1) return;

            await _blackImage.DOFade(0, duration).SetEase(ease).ToUniTask(cancellationToken: ct);
        }

        public void NewlyShowLogText(string text, float duration)
        {
            if (_logText == null) return;

            _ctsLogText.Cancel();
            _logText.text = string.Empty;

            _ctsLogText.Dispose();
            _ctsLogText = new();
            ShowLogText(_logText, text, duration, _ctsLogText.Token).Forget();

            static async UniTaskVoid ShowLogText
                (TextMeshProUGUI logText, string text, float duration, CancellationToken ct)
            {
                logText.text = text;
                await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: ct);
                logText.text = string.Empty;
            }
        }

        public void ActivateUIManagers(CancellationToken ct)
        {
            if (_managePlayerUI != null) _managePlayerUI.RollItem(ct).Forget();
            if (_triggerPauseUI != null) _triggerPauseUI.Trigger(ct).Forget();
            if (_triggerSettingUI != null) _triggerSettingUI.ChangeVolume(ct).Forget();
        }

        public void SetCursor(bool isActive)
        {
            if (_triggerPauseUI == null) return;
            _triggerPauseUI.SetCursor(isActive);
        }
    }
}