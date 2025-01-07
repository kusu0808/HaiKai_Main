using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ShrineWayFoundByYatsu(CancellationToken ct)
        {
            _borders.IsFromUnderStageToShrineWayBorderEnabled = true;
            await UniTask.WaitUntil(() => _borders.ShrineWayFoundedEvent.IsIn(_player.Position) is true, cancellationToken: ct);
            await DoEvent(ct);

            async UniTask DoEvent(CancellationToken ct)
            {
                _player.IsPlayerControlEnabled = false;
                _player.IsVisible = false;

                await UniTask.WhenAll(
                    _objects.ShrineWayFoundByYatsuTimeline.PlayOnce(ct),
                    OnPlaying(ct)
                );
                _yatsu.SpawnHere(_points.ShrineWayYatsuSpawnPoint);

                _player.IsVisible = true;
                _player.IsPlayerControlEnabled = true;
            }

            async UniTask OnPlaying(CancellationToken ct)
            {
                await UniTask.NextFrame(cancellationToken: ct);

                _player.SetTransform(_points.ShrineWayPlayerTeleportPoint);

                _objects.ShrineWayRock.IsEnabled = true;
                _borders.IsFromUnderStageToShrineWayBorderEnabled = false;
                _objects.VillageWayCannotGoBackAfterWarehouse.IsEnabled = true;
            }
        }
    }
}