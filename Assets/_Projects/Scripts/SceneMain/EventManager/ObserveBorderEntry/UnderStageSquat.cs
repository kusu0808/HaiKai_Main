using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using IA;
using Main.Eventer;

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
                bool IsInAny(out Borders.TeleportBorders.Element element)
                {
                    Func<Borders.TeleportBorders.Element> f = _borders.IsFromUnderStageToShrineWayBorderEnabled ?
                    () => _borders.UnderStageSquat.IsInAny(_player.Position, isEnter) :
                    () => _borders.UnderStageSquat.IsInAny(_player.Position, isEnter, 0);

                    element = f();
                    return element is not null;
                }

                while (true)
                {
                    Borders.TeleportBorders.Element element = null;
                    await UniTask.WaitUntil(() => IsInAny(out var element), cancellationToken: ct);

                    _uiElements.LogText.ShowManually(isEnter ? "(アクション長押しで入る)" : "(アクション長押しで出る)");
                    int j = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => element.GetBorder(isEnter).IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                    _uiElements.LogText.ShowManually(string.Empty);

                    if (j is not 1) continue;
                    await _TeleportPlayer(element.GetTransform(isEnter), ct);
                }
            }
        }
    }
}
