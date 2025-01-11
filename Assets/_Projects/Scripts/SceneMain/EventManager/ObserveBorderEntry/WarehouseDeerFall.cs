using Cysharp.Threading.Tasks;
using System.Threading;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid WarehouseDeerFall(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.WarehouseDeerFall.IsIn(_player.Position) is true, cancellationToken: ct);
            _objects.Deers.Fall();
        }
    }
}
