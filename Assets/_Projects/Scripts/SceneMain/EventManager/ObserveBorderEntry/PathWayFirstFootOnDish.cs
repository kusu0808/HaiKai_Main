using Cysharp.Threading.Tasks;
using System.Threading;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWayFirstFootOnDish(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.PathWayFirstFootOnDish.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.SE.DishBreak, SoundType.SE);
            _audioSources.GetNew().Raise(_audioClips.Voice.DaughterScream, SoundType.Voice);
        }
    }
}
