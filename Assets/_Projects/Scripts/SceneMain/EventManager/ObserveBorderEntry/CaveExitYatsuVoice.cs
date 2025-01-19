using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid CaveExitYatsuVoice(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.CaveExitYatsuVoice.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.YaTsuShoutingVoice, SoundType.Voice);
        }
    }
}