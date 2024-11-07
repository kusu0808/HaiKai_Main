using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTask VillageWayBirdAppear(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.VillageWayBirdFly.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.BirdCry, SoundType.Voice);
            _audioSources.GetNew().Raise(_audioClips.SE.BirdFlyAway, SoundType.SE);

            "鳥を飛び立たせる".Warn();
        }
    }
}