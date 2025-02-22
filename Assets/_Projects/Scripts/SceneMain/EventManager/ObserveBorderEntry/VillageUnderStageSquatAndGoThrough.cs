using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using IA;
using Element = Main.Eventer.Borders.TeleportBorders.Element;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageUnderStageSquatAndGoThrough(CancellationToken ct)
        {
            Impl(true, ct).Forget();
            Impl(false, ct).Forget();

            async UniTaskVoid Impl(bool isEnter, CancellationToken ct)
            {
                bool IsInAny(out Element element)
                {
                    Func<Element> f = _borders.IsFromUnderStageToShrineWayBorderEnabled ?
                    () => _borders.UnderStageSquat.IsInAny(_player.Position, isEnter) :
                    () => _borders.UnderStageSquat.IsInAny(_player.Position, isEnter, 0);

                    element = f();
                    return element is not null;
                }

                async UniTask<Element> WaitUntilIsInAny(CancellationToken ct)
                {
                    while (true)
                    {
                        bool ret = IsInAny(out var element);
                        if (ret) return element;
                        await UniTask.NextFrame(ct);
                    }
                }

                while (true)
                {
                    var element = await WaitUntilIsInAny(ct);

                    _uiElements.LogText.ShowManually(isEnter ? "[ くぐる(右クリック) ]" : "[ ぬける(右クリック) ]");
                    int j = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => element.GetBorder(isEnter).IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancelWhileUnpause, cancellationToken: ct));
                    _uiElements.LogText.ShowManually(string.Empty);

                    if (j is not 1) continue;
                    await _TeleportPlayer(element.GetTransform(isEnter), ct);
                }
            }
        }
    }
}
