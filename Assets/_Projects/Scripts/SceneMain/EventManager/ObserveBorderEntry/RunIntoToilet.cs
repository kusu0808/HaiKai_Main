using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid RunIntoToilet(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.VillageFarWayRunIntoToilet.IsIn(_player.Position) is true, cancellationToken: ct);

            _yatsu.Despawn();
            await _TeleportPlayer(_points.VillageFarWayInsideToiletPoint, ct);

            if (_yatsuKnockToiletDoorAudioSource != null) return;
            _yatsuKnockToiletDoorAudioSource = _audioSources.GetNew();
            _yatsuKnockToiletDoorAudioSource.Raise(_audioClips.BGM.YatsuKnockToiletDoor, SoundType.BGM);
        }
    }
}