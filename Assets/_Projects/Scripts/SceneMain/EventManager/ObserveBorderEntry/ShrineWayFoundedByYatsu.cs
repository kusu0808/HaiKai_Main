using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ShrineWayFoundedByYatsu(CancellationToken ct)
        {
            _borders.IsFromUnderStageToShrineWayBorderEnabled = true;
            await UniTask.WaitUntil(() => _borders.ShrineWayFoundedEvent.IsIn(_player.Position) is true, cancellationToken: ct);
            _player.IsPlayerControlEnabled = false;
            "ヤツに見つかったイベントを再生する！(とりあえず、適当に3秒待つ)".Warn();
            await UniTask.Delay(3000, cancellationToken: ct);
            "岩で参道を下れないようにする".Warn();
            _borders.IsFromUnderStageToShrineWayBorderEnabled = false;
            "ヤツの方を向かせた方がいいか？".Warn();
            _player.IsPlayerControlEnabled = true;
        }
    }
}