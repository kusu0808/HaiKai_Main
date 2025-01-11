using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid CavePickUpKeyInDoorPuzzleSolving(CancellationToken ct)
        {
            if (_objects.KeyInDoorPuzzleSolving.IsEnabled is false) return;

            PauseState.IsPaused = true;
            "失われた鍵入手開始".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "失われた入手終了".Warn();
            PauseState.IsPaused = false;

            _uiElements.KeyInDoorPuzzleSolving.Obtain();
            _objects.KeyInDoorPuzzleSolving.IsEnabled = false;
        }
    }
}