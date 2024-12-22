using System.Threading;
using BorderSystem;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayYatsuDaughterVoice(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.IsFromUnderStageToShrineWayBorderEnabled is false,
            cancellationToken: ct);

            await UniTask.WaitUntil(() => _borders.VillageFarWayYatsuDaughterVoice1.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.YaTsuImitateDaughterVoice, SoundType.Voice);

            await UniTask.WaitUntil(() => _borders.VillageFarWayYatsuDaughterVoice2.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.YaTsuImitateDaughterVoice, SoundType.Voice);
        }
    }
}