using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid FromShrineUpWayToVillageFarWayEscapeFromYatsu(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.ShrineUpWayYatsuComeFromEscapeRoute.IsIn(_player.Position) is true, cancellationToken: ct);
            _yatsu.SpawnHere(_points.ShrineUpWayYatsuComeFromEscapeRouteSpawnPoint);

            await UniTask.WaitUntil(() => _borders.VillageFarWayYatsuComeFromCave.IsIn(_player.Position) is true, cancellationToken: ct);
            _yatsu.SpawnHere(_points.VillageFarWayYatsuComeFromCaveSpawnPoint);
        }
    }
}