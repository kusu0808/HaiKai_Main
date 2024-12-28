using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PickUpSecretKey(CancellationToken ct)
        {
            if (_objects.KokeshiSecretKey.IsEnabled is false) return;

            PauseState.IsPaused = true;
            "隠し鍵入手開始".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "隠し鍵入手終了".Warn();
            PauseState.IsPaused = false;

            _uiElements.KokeshiSecretKey.Obtain();
            _objects.KokeshiSecretKey.IsEnabled = false;

            _objects.IsPickUpSecretKeyEventEnabled = false;
        }
    }
}