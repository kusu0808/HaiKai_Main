using System.Threading;
using BorderSystem;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWayDaughterSaysGoAhead(CancellationToken ct)
        {
            Border border = _borders.PathWayDaughterSaysGoAhead;

            await UniTask.WaitUntil(() => border.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.DaughterRequestGoing, SoundType.Voice);
        }
    }
}