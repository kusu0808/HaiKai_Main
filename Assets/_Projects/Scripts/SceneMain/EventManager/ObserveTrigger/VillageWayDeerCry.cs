using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTask VillageWayDeerCry(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.VillageWayDeerCry1.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.DeerCry, SoundType.Voice);
            await UniTask.WaitUntil(() => _borders.VillageWayDeerCry2.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.DeerCry, SoundType.Voice);
        }
    }
}