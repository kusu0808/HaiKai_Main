using Cysharp.Threading.Tasks;
using System.Threading;
using IA;
using General;
using TeleportBorder = Main.Eventer.Borders.TeleportBorder;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWaySquat(CancellationToken ct)
        {
            bool isSeparatedFromDaughter = false; // 娘と別れたか

            while (true)
            {
                int i = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => _borders.PathWaySquat1.In.IsIn(_player.Position) is true, cancellationToken: ct),
                    UniTask.WaitUntil(() => _borders.PathWaySquat2.In.IsIn(_player.Position) is true, cancellationToken: ct));

                bool isGoingToTheBack = i is 0; // 奥の道に向かっているか
                bool isFarewellTurn = isGoingToTheBack is true && isSeparatedFromDaughter is false; // 娘と別れるターンか

                TeleportBorder cache = isGoingToTheBack ? _borders.PathWaySquat1 : _borders.PathWaySquat2;

                string text = isFarewellTurn ? "ここ、すごく狭いね (アクション長押しで通る)" : "(アクション長押しで通る)";
                _uiElements.LogText.ShowManually(text);
                int j = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => cache.In.IsIn(_player.Position) is false, cancellationToken: ct),
                    UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                _uiElements.LogText.ShowManually(string.Empty);
                if (j is not 1) continue;

                await _TeleportPlayer(cache.FirstTf, ct);
                await UniTask.WaitUntil(() => cache.Out.IsIn(_player.Position) is true, cancellationToken: ct);
                await _TeleportPlayer(cache.SecondTf, ct);

                if (isFarewellTurn is false) continue;
                isSeparatedFromDaughter = true;
                _daughter.Despawn();
                _daughter.IsKnifeEnabled = true;
                _audioSources.GetNew().Raise(_audioClips.Voice.DaughterScream, SoundType.Voice);
            }
        }
    }
}