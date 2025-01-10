using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid OpenToiletLockedDoor(CancellationToken ct)
        {
            if (_isOpenToiletLockedDoorEventEnabled is true)
            {
                if (_uiElements.WarehouseKeyDoubled.IsHolding() is false)
                {
                    _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                    return;
                }

                _isOpenToiletLockedDoorEventEnabled = false;

                _uiElements.WarehouseKeyDoubled.Release();
                _uiElements.WarehouseKey.Obtain();

                _uiElements.LogText.ShowAutomatically("鍵を開けた");

                _yatsu.Despawn();
                await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
                await _TeleportPlayer(_points.VillageFarWayInsideToiletPoint, ct);

                if (_yatsuKnockToiletDoorAudioSource != null) return;
                _yatsuKnockToiletDoorAudioSource = _audioSources.GetNew();
                _yatsuKnockToiletDoorAudioSource.Raise(_audioClips.BGM.YatsuKnockToiletDoor, SoundType.BGM);

                _hasRunAwayFromFirstYatsu = true;
            }
            else
            {
                if (_isPickUpSecretKeyEventEnabled is true)
                {
                    if (_hasRunAwayFromFirstYatsu is true)
                    {
                        _uiElements.LogText.ShowAutomatically("化け物がいる、出るわけにはいかない");
                    }

                    return;
                }

                if (_hasDecidedNotToTurnBack is true) return;
                _hasDecidedNotToTurnBack = true;

                _uiElements.LogText.ShowAutomatically("ここまで来たらもう引き返せない");

                await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
                await _TeleportPlayer(_points.VillageFarWayOutsideToiletPoint, ct);
            }
        }
    }
}