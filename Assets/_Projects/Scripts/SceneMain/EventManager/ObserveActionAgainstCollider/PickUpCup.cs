using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PickUpCup(CancellationToken ct)
        {
            if (_objects.ToiletCup.IsEnabled is false) return;

            PauseState.IsPaused = true;
            "コップ入手開始".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "コップ入手終了".Warn();
            PauseState.IsPaused = false;

            _playerItem.HasCup = true;
            _objects.ToiletCup.IsEnabled = false;
            _uiElements.IsShowCup = true;
        }
    }
}