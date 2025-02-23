using Cysharp.Threading.Tasks;
using System.Threading;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayYatsuStepOnGlassPiece(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _isPickUpSecretKeyEventEnabled is false, cancellationToken: ct);
            await UniTask.WaitUntil(() => _objects.VillageFarWayScatteredGlassPiece.IsEnabled is true, cancellationToken: ct);
            await UniTask.WhenAll(
                UniTask.WaitUntil(() => _yatsu.IsEnabled is true, cancellationToken: ct),
                UniTask.WaitUntil(() => _borders.VillageFarWayGlassShardArea.IsIn(_yatsu.Position) is true, cancellationToken: ct)
            );

            _yatsu.IsSlow = true;
            _audioSources.GetNew().Raise(_audioClips.Voice.YaTsuDamagedVoice, SoundType.Voice);
            await UniTask.WaitForSeconds(1.0f, cancellationToken: ct);
            _yatsu.Despawn();
            _yatsu.IsSlow = false;
        }
    }
}