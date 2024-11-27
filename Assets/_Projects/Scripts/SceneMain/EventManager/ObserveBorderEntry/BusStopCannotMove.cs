using Cysharp.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using BorderSystem;
using Main.Eventer;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid BusStopCannotMove(CancellationToken ct)
        {
            ReadOnlyCollection<Border> cache = _borders.BusStopCannotMove.Elements;

            while (true)
            {
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is false, cancellationToken: ct);
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is true, cancellationToken: ct);
                _uiElements.NewlyShowLogText("真っ暗で、先が見えない…");
                await UniTask.Delay(TimeSpan.FromSeconds(EventManagerConst.SameEventDuration), cancellationToken: ct);
            }
        }
    }
}