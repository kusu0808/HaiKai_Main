using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayYatsuStopToiletKnock(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.VillageFarWayYatsuStopToiletSound.IsIn(_player.Position) is true, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.VillageFarWayYatsuStopToiletSound.IsIn(_player.Position) is false, cancellationToken: ct);

            if (_yatsuKnockToiletDoorAudioSource == null) return;
            _yatsuKnockToiletDoorAudioSource.Pause();
        }
    }
}