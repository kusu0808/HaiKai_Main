using System.Threading;
using Cysharp.Threading.Tasks;
using IA;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayScatterGlassPiece(CancellationToken ct)
        {
            var border = _borders.VillageFarWayGlassShardArea;

            await UniTask.WaitUntil(() => _uiElements.GlassShard.HasItem() is true, cancellationToken: ct);

            while (true)
            {
                await UniTask.WaitUntil(() => border.IsIn(_player.Position) is true, cancellationToken: ct);
                TriggerPauseUI.IsInputEnabled = false;
                _uiElements.LogText.ShowManually("[ ガラス片を撒く(右クリック) ]");
                int i = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool is true, cancellationToken: ct),
                    UniTask.WaitUntil(() => border.IsIn(_player.Position) is false, cancellationToken: ct)
                );
                _uiElements.LogText.ShowManually(string.Empty);
                TriggerPauseUI.IsInputEnabled = true;

                if (i == 0)
                {
                    _uiElements.GlassShard.Release();
                    _objects.VillageFarWayScatteredGlassPiece.IsEnabled = true;
                    _uiElements.LogText.ShowAutomatically("ガラス片を撒いた");
                    break;
                }
            }
        }
    }
}