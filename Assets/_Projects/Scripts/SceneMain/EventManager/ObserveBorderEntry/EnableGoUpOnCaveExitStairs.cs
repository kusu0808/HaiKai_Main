using Cysharp.Threading.Tasks;
using Main.Eventer.Borders;
using System.Threading;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid EnableGoUpOnCaveExitStairs(CancellationToken ct)
        {
            MultiBorders cache = _borders.EnableGoUpOnCaveExitStairs;

            while (true)
            {
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is true, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitOnCaveExitStairs;
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is false, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitInit;
            }
        }
    }
}
