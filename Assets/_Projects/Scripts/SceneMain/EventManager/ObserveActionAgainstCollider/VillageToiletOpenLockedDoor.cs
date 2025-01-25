using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageToiletOpenLockedDoor(CancellationToken ct)
        {
            if (_isOpenToiletLockedDoorEventEnabled is true)
            {
                if (_uiElements.WarehouseKeyDoubled.IsHolding() is true)
                {
                    _isOpenToiletLockedDoorEventEnabled = false;

                    _uiElements.WarehouseKeyDoubled.Release();
                    _uiElements.WarehouseKey.Obtain();

                    _uiElements.LogText.ShowAutomatically("鍵を開けた");

                    _yatsu.Despawn();
                    await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
                    await _TeleportPlayer(_points.VillageFarWayInsideToiletPoint, ct);

                    AudioSource audioSource = _audioSources.VillageToiletYatsuKnockDoor;
                    if (audioSource != null) audioSource.Raise(_audioClips.BGM.YatsuKnockToiletDoor, SoundType.BGM);

                    _hasRunAwayFromFirstYatsu = true;
                }
                else if (_uiElements.IsHoldingAnyItem() is true)
                {
                    _uiElements.LogText.ShowAutomatically("鍵を開けられるものはないだろうか？");
                }
                else
                {
                    _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                }
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

                _uiElements.LogText.ShowAutomatically("ここまで来たらもう引き返せない、先に進もう");

                await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
                await _TeleportPlayer(_points.VillageFarWayOutsideToiletPoint, ct);
            }
        }
    }
}