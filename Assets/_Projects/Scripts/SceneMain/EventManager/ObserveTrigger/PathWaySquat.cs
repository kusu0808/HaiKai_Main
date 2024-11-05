using Cysharp.Threading.Tasks;
using System.Threading;
using IA;
using Main.Eventer;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWaySquat(CancellationToken ct)
        {
            while (true)
            {
                int i = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => _borders.PathWaySquat1.In.IsIn(_player.Position) is true, cancellationToken: ct),
                    UniTask.WaitUntil(() => _borders.PathWaySquat2.In.IsIn(_player.Position) is true, cancellationToken: ct));

                Borders.TeleportBorder cache = i is 0 ? _borders.PathWaySquat1 : _borders.PathWaySquat2;
                string logText = i is 0 ? "ここ、すごく狭いね (アクション長押しで通る)" : "そろそろ戻ろう (アクション長押しで通る)";

                _uiElements.ForciblyShowLogText(logText);
                int j = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => cache.In.IsIn(_player.Position) is false, cancellationToken: ct),
                    UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                _uiElements.ForciblyShowLogText(string.Empty);
                if (j is not 1) continue;

                await _TeleportPlayer(cache.FirstTf, ct);
                await UniTask.WaitUntil(() => cache.Out.IsIn(_player.Position) is true, cancellationToken: ct);
                await _TeleportPlayer(cache.SecondTf, ct);
            }
        }
    }
}