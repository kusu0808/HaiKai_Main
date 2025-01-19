using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTask VillageWayDeerAppear(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.VillageWayDeerJumpOut.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.DeerCry, SoundType.Voice);
            _audioSources.GetNew().Raise(_audioClips.SE.DeerJumpOut, SoundType.SE);

            "鹿が逃げていく".Warn();
            await UniTask.Delay(1000, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.SE.DeerRunAway, SoundType.SE);
        }
    }
}