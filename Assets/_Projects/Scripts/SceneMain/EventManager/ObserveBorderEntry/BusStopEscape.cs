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

            TriggerPauseUI.IsInputEnabled = false;
            _player.IsPlayerControlEnabled = false;
            _player.IsVisible = false;
            _isWalkingSoundMuted.Value = true;
            _player.IsCameraEaseCut = true;

            await _objects.BusStopEscapeTimeline.PlayOnce(ct);

            // 一応、フラグを元に戻す
            _player.IsCameraEaseCut = false;
            _isWalkingSoundMuted.Value = false;
            _player.IsVisible = true;
            _player.IsPlayerControlEnabled = true;
            TriggerPauseUI.IsInputEnabled = true;

            Scene.ID.Result.LoadAsync().Forget();
        }
    }
}