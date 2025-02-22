using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayCutIvyYatsuComeFromCave(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _hasDecidedNotToTurnBack is true, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.VillageFarWayCutIvyYatsuComeFromCave.IsIn(_player.Position) is true, cancellationToken: ct);

            _yatsu.SpawnHere(_points.VillageFarWayOnCutIvyYatsuSpawnPoint);
        }
    }
}