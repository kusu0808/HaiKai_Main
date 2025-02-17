using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid BusStopEscape(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _hasSavedDaughter is true, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.BusStopEscape.IsIn(_player.Position) is true, cancellationToken: ct);

            _yatsu.Despawn();
            _daughter.Despawn();

            TriggerPauseUI.IsInputEnabled = false;
            _player.IsPlayerControlEnabled = false;
            _player.IsVisible = false;
            _isWalkingSoundMuted.Value = true;
            _player.IsCameraEaseCut = true;

            _objects.EndingCutScene.TranseBlackBorder(ct).Forget();
            _objects.EndingCutScene.TranseTimeScale(ct).Forget();
            await _objects.BusStopEscapeTimeline.PlayOnce(ct, deactivateAfterPlayed: false);
            await _uiElements.BlackImage.FadeOut(1.5f, ct);

            Scene.ID.Title.LoadAsync().Forget();
        }
    }
}