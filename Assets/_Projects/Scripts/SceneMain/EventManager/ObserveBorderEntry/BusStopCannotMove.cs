using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Main.Eventer;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid BusStopCannotMove(CancellationToken ct)
        {
            Borders.MultiBorders cache = _borders.BusStopCannotMove;

            while (true)
            {
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is false, cancellationToken: ct);
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is true, cancellationToken: ct);
                _uiElements.LogText.ShowAutomatically("真っ暗で、先が見えない…");
                await UniTask.Delay(TimeSpan.FromSeconds(EventManagerConst.SameEventDuration), cancellationToken: ct);
            }
        }
    }
}