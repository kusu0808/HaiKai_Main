using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayYatsuDaughterVoice(CancellationToken ct)
        {
            if (_uiElements.KokeshiSecretKey.IsHolding() is false) return;

            await UniTask.WaitUntil(() => _borders.VillageWayCannotGoBackAfterWarehouse.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.Voice.YaTsuImitateDaughterVoice, SoundType.Voice);
        }
    }
}