using System;
using System.Threading;
using BorderSystem;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTask ShrineUpWayDaughterSaysNotHere(CancellationToken ct)
        {
            Border cache = _borders.ShrineUpWayDaughterSaysNotHere;

            while (true)
            {
                await UniTask.WaitUntil(() => _borders.IsFromUnderStageToShrineWayBorderEnabled is false,
                cancellationToken: ct);
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is false, cancellationToken: ct);
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is true, cancellationToken: ct);
                _uiElements.LogText.ShowAutomatically("そっちじゃない！");
                await UniTask.Delay(TimeSpan.FromSeconds(EventManagerConst.SameEventDuration), cancellationToken: ct);
            }
        }
    }
}