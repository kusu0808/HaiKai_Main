using BorderSystem;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid EnableGoUpOnEnteringHouse(CancellationToken ct)
        {
            Border cache = _borders.EnableGoUpOnEnteringHouse;

            while (true)
            {
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is true, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitOnEnteringHouse;
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is false, cancellationToken: ct);
                _player.SlopLimit = EventManagerConst.SlopLimitInit;
            }
        }
    }
}
