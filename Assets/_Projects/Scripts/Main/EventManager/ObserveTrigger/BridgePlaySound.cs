using Cysharp.Threading.Tasks;
using System.Threading;
using BorderSystem;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid BridgePlaySound(CancellationToken ct)
        {
            Border cache = _borders.BridgePlaySound;

            while (true)
            {
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is true, cancellationToken: ct);
                "後方置換：橋がきしむ音を再生開始".Warn();
                await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is false, cancellationToken: ct);
                "後方置換：橋がきしむ音を再生終了".Warn();
            }
        }
    }
}