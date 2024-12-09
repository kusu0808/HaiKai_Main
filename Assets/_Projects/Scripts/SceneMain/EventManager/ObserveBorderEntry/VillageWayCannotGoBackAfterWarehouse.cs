using System;
using System.Threading;
using BorderSystem;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTask VillageWayCannotGoBackAfterWarehouse(CancellationToken ct)
        {
            Border cache = _borders.VillageWayCannotGoBackAfterWarehouse;

            while (true)
            {
                await UniTask.WaitUntil(() => _borders.IsFromUnderStageToShrineWayBorderEnabled is false,
                cancellationToken: ct);
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is false, cancellationToken: ct);
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is true, cancellationToken: ct);
                _uiElements.LogText.ShowAutomatically("娘を助けなくては…");
                await UniTask.Delay(TimeSpan.FromSeconds(EventManagerConst.SameEventDuration), cancellationToken: ct);
            }
        }
    }
}