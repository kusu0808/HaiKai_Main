using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using IA;
using Main.Eventer;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid UnderStageSquat(CancellationToken ct)
        {
            while (true)
            {
                int i1 = 0;
                if (_borders.IsFromUnderStageToShrineWayBorderEnabled)
                    i1 = await WaitUntilConditionReturnMinusOne(() => _borders.UnderStageSquat.Elements.IsInInAny(_player.Position), ct);
                else
                    i1 = await WaitUntilConditionReturnMinusOne(() => _borders.UnderStageSquat.Elements.IsInInAny(_player.Position, 0), ct);
                if (i1 is -1) return;
                var cache1 = _borders.UnderStageSquat.Elements[i1];
                _uiElements.ForciblyShowLogText("(アクション長押しで入る)");
                int j1 = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => cache1.In.IsIn(_player.Position) is false, cancellationToken: ct),
                    UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                _uiElements.ForciblyShowLogText(string.Empty);
                if (j1 is not 1) continue;
                await _TeleportPlayer(cache1.OutTf, ct);

                int i2 = 0;
                if (_borders.IsFromUnderStageToShrineWayBorderEnabled)
                    i2 = await WaitUntilConditionReturnMinusOne(() => _borders.UnderStageSquat.Elements.IsInOutAny(_player.Position), ct);
                else
                    i2 = await WaitUntilConditionReturnMinusOne(() => _borders.UnderStageSquat.Elements.IsInOutAny(_player.Position, 0), ct);
                if (i2 is -1) return;
                var cache2 = _borders.UnderStageSquat.Elements[i2];
                _uiElements.ForciblyShowLogText("(アクション長押しで出る)");
                int j2 = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => cache2.Out.IsIn(_player.Position) is false, cancellationToken: ct),
                    UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                _uiElements.ForciblyShowLogText(string.Empty);
                if (j2 is not 1) continue;
                await _TeleportPlayer(cache2.OutTf, ct);
            }

            // condition が -1 以外を返すまで待機し、そのインデックスを返す
            static async UniTask<int> WaitUntilConditionReturnMinusOne(Func<int> condition, CancellationToken ct)
            {
                if (condition is null) return -1;
                while (true)
                {
                    int i = condition();
                    if (i is not -1) return i;
                    await UniTask.Yield(ct);
                }
            }
        }
    }
}
