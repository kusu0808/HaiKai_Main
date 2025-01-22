using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid CaveGokiChanAppear(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _hasDecidedNotToTurnBack is true, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.CaveGokiChanAppear1.IsIn(_player.Position) is true, cancellationToken: ct);
            await _objects.GokiChan.TraceNext(ct);
            await UniTask.WaitUntil(() => _borders.CaveGokiChanAppear2.IsIn(_player.Position) is true, cancellationToken: ct);
            await _objects.GokiChan.TraceNext(ct);
        }
    }
}