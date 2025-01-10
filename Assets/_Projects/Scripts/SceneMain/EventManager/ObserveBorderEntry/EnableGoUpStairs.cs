using Cysharp.Threading.Tasks;
using Main.Eventer.Borders;
using System.Threading;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid EnableGoUpStairs(CancellationToken ct)
        {
            var borders = MultiBorders.New(
                _borders.EnableGoUpOnEnteringHouse,
                _borders.EnableGoUpOnShrineWay,
                _borders.EnableGoUpOnWarehouseStairs,
                _borders.EnableGoUpOnCaveStairs,
                _borders.EnableGoUpOnCaveExitStairs
            );

            while (true)
            {
                await UniTask.WaitUntil(() => borders.IsInAny(_player.Position) is true, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitOnStairs;
                await UniTask.WaitUntil(() => borders.IsInAny(_player.Position) is false, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitInit;
            }
        }
    }
}
