using Cysharp.Threading.Tasks;
using Main.Eventer.Borders;
using System.Threading;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid EnableGoUpOnCaveStairs(CancellationToken ct)
        {
            MultiBorders cache = _borders.EnableGoUpOnCaveStairs;

            while (true)
            {
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is true, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitOnCaveStairs;
                await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is false, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitInit;
            }
        }
    }
}
