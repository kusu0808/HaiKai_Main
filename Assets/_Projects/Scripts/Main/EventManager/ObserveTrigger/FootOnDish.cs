using Cysharp.Threading.Tasks;
using System.Threading;
using BorderSystem;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid FootOnDish(CancellationToken ct)
        {
            Border cache = _borders.FootOnDish;

            bool IsMovingOnDish() => cache.IsIn(_player.Position) is true && _player.IsMoving is true;

            while (true)
            {
                await UniTask.WaitUntil(() => IsMovingOnDish() is true, cancellationToken: ct);
                "後方置換：皿が割れる音を再生開始".Warn();
                await UniTask.WaitUntil(() => IsMovingOnDish() is false, cancellationToken: ct);
                "後方置換：皿が割れる音を再生終了".Warn();
            }
        }
    }
}