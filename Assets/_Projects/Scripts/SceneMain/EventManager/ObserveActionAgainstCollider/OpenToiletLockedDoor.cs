using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid OpenToiletLockedDoor(CancellationToken ct)
        {
            if (_uiElements.WarehouseKeyDoubled.IsHolding() is false)
            {
                _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                return;
            }

            _uiElements.WarehouseKeyDoubled.Release();
            _uiElements.WarehouseKey.Obtain();

            _uiElements.LogText.ShowAutomatically("鍵を開けた");

            _yatsu.Despawn();
            await _TeleportPlayer(_points.VillageFarWayInsideToiletPoint, ct);

            if (_yatsuKnockToiletDoorAudioSource != null) return;
            _yatsuKnockToiletDoorAudioSource = _audioSources.GetNew();
            _yatsuKnockToiletDoorAudioSource.Raise(_audioClips.BGM.YatsuKnockToiletDoor, SoundType.BGM);
        }
    }
}