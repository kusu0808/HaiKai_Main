using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PickUpButaiSideKey(CancellationToken ct)
        {
            if (_objects.WarehouseKeyDoubled.IsEnabled is false) return;

            PauseState.IsPaused = true;
            "舞台横の鍵入手開始".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "舞台横の鍵入手終了".Warn();
            PauseState.IsPaused = false;

            _uiElements.WarehouseKeyDoubled.Obtain();
            _objects.WarehouseKeyDoubled.IsEnabled = false;
        }
    }
}