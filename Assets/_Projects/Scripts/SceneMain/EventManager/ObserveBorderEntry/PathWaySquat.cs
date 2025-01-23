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

                TeleportBorder border = isGoingToTheBack ? _borders.PathWaySquat1 : _borders.PathWaySquat2;

                _uiElements.LogText.ShowManually("[ くぐる(右クリック) ]");
                int j = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => border.In.IsIn(_player.Position) is false, cancellationToken: ct),
                    UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool, cancellationToken: ct));
                _uiElements.LogText.ShowManually(string.Empty);
                if (j is not 1) continue;

                await _TeleportPlayer(border.FirstTf, ct);
                while (true)
                {
                    await UniTask.WaitUntil(() => border.Out.IsIn(_player.Position) is true, cancellationToken: ct);
                    _uiElements.LogText.ShowManually("[ ぬける(右クリック) ]");
                    int k = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => border.Out.IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool, cancellationToken: ct));
                    _uiElements.LogText.ShowManually(string.Empty);
                    if (k is 1) break;
                }
                await _TeleportPlayer(border.SecondTf, ct);

                if (isFarewellTurn is false) continue;
                isSeparatedFromDaughter = true;
                _daughter.Despawn();
                _objects.DaughterKnife.IsEnabled = true;
                _audioSources.GetNew().Raise(_audioClips.Voice.DaughterKidnapped, SoundType.Voice);
            }
        }
    }
}