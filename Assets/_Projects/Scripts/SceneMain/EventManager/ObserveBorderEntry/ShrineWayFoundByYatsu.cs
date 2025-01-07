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

            _player.IsPlayerControlEnabled = false;
            await UniTask.WhenAll(
                _objects.ShrineWayFoundByYatsuTimeline.PlayOnce(ct),
                WhenCutScene(ct)
            );
            _yatsu.SpawnHere(_points.ShrineWayYatsuSpawnPoint);
            _player.SetTransform(_points.ShrineWayPlayerTeleportPoint);
            _player.IsPlayerControlEnabled = true;



            async UniTask WhenCutScene(CancellationToken ct)
            {
                await UniTask.NextFrame(cancellationToken: ct);

                _objects.ShrineWayRock.IsEnabled = true;
                _borders.IsFromUnderStageToShrineWayBorderEnabled = false;
                _objects.VillageWayCannotGoBackAfterWarehouse.IsEnabled = true;
            }
        }
    }
}