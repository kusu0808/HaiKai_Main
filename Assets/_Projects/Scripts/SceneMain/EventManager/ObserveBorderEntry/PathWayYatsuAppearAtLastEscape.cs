using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWayYatsuAppearAtLastEscape(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _hasSavedDaughter is true, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.PathWayYatsuAppearAtLastEscape.IsIn(_player.Position) is true, cancellationToken: ct);

            TriggerPauseUI.IsInputEnabled = false;
            _player.IsPlayerControlEnabled = false;
            _player.IsVisible = false;
            _isWalkingSoundMuted.Value = true;

            await _uiElements.BlackImage.FadeOut(0.5f, ct);
            _player.SetTransform(_points.PathWayPlayerTeleportPointAtLastEscape);
            _yatsu.SpawnHere(_points.PathWayYatsuComeAtLastEscapeSpawnPoint);
            _daughter.SpawnHere(_points.PathWayDaughterAtLastEscapeSpawnPoint);
            _audioSources.GetNew().Raise(_audioClips.Voice.DaughterCall, SoundType.Voice);
            await _uiElements.BlackImage.FadeIn(0.5f, ct);

            _isWalkingSoundMuted.Value = false;
            _player.IsVisible = true;
            _player.IsPlayerControlEnabled = true;
            TriggerPauseUI.IsInputEnabled = true;
        }
    }
}