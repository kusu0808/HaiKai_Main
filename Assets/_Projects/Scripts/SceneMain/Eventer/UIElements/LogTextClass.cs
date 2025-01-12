using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using IA;

namespace Main.Eventer.UIElements
{
    [Serializable]
    public sealed class LogTextClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _logText;
        [SerializeField, Required, SceneObjectsOnly]
        // private Image _logTextImage;

        private const float SHOW_DURATION_DEFAULT = 3;
        private const float FADEOUT_DURATION_DEFAULT = 1;

        private bool _isShowingForcibly = false;

        private CancellationTokenSource _cts = new();
        private void ResetCts()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = new();
        }

        /// <summary>
        /// 手動でログの表示と非表示を行う。
        /// textが null or Empty の場合、ログテキストを非表示にしたとみなす。
        /// NewlyShowLogText()を強制的に止め、ログを表示する。
        /// 表示している間、NewlyShowLogText()の実行は無効化される。
        /// </summary>
        public void ShowManually(string text)
        {
            if (_logText == null) return;
            _isShowingForcibly = !string.IsNullOrEmpty(text);

            ResetCts();
            _logText.text = text;
            _logText.alpha = 1;
        }

        /// <summary>
        /// 自動でログの表示と非表示を行う。
        /// </summary>
        public void ShowAutomatically(
            string text, float duration = SHOW_DURATION_DEFAULT, float fadeoutDuration = FADEOUT_DURATION_DEFAULT, bool isGetOffInput = false)
        {
            if (_logText == null) return;
            if (_isShowingForcibly) return;

            ResetCts();
            _logText.text = string.Empty;
            ShowLogText(_logText, /*_logTextImage,*/ text, duration, fadeoutDuration, _cts.Token, isGetOffInput).Forget();

            static async UniTaskVoid ShowLogText
                (TextMeshProUGUI logText, /*Image logTextImage,*/ string text, float duration, float fadeoutDuration, CancellationToken ct, bool isGetOffInput = false)
            {
                // logTextImage.color = new Color(0,0,0,0.5f);
                logText.text = text;
                if (isGetOffInput) await UniTask.WhenAny(WaitUntilOffInput(ct),
                    UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: ct));
                else await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: ct);
                await logText.DOFade(0, fadeoutDuration).ToUniTask(cancellationToken: ct);
                logText.text = string.Empty;
                logText.alpha = 1;
                //logTextImage.color = Color.clear;
            }

            static async UniTask WaitUntilOffInput(CancellationToken ct) =>
                await UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool, cancellationToken: ct);
        }
    }
}