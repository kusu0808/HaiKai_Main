using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using IA;
using Main.Eventer;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void UnderStageSquat(CancellationToken ct)
        {
            Impl(true, ct).Forget();
            Impl(false, ct).Forget();

            async UniTaskVoid Impl(bool isEnter, CancellationToken ct)
            {
                while (true)
                {
                    Func<int> f = _borders.IsFromUnderStageToShrineWayBorderEnabled ?
                    () => _borders.UnderStageSquat.Elements.IsInAny(_player.Position, isEnter) :
                    () => _borders.UnderStageSquat.Elements.IsInAny(_player.Position, isEnter, 0);

                    int i = await WaitUntilValid(f, ct);
                    if (i is -1) continue;
                    var element = _borders.UnderStageSquat.Elements[i];

                    _uiElements.ForciblyShowLogText(isEnter ? "(アクション長押しで入る)" : "(アクション長押しで出る)");
                    int j = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => element.GetBorder(isEnter).IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                    _uiElements.ForciblyShowLogText(string.Empty);

                    if (j is not 1) continue;
                    await _TeleportPlayer(element.GetTransform(isEnter), ct);
                }

                // f が -1 以外を返すまで待機し、そのインデックスを返す
                static async UniTask<int> WaitUntilValid(Func<int> f, CancellationToken ct)
                {
                    if (f is null) return -1;
                    while (true)
                    {
                        int i = f();
                        if (i is not -1) return i;
                        await UniTask.NextFrame(ct);
                    }
                }
            }
        }
    }
}
