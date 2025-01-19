using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayCutIvyYatsuComeFromCave(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.IsFromUnderStageToShrineWayBorderEnabled is false, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.VillageFarWayCutIvyYatsuComeFromCave.IsIn(_player.Position) is true, cancellationToken: ct);

#if false
            _yatsu.SpawnHere(_points.VillageFarWayOnCutIvyYatsuSpawnPoint);
#else
            "リザルトシーンに遷移します".Warn();
            Scene.ID.Result.LoadAsync().Forget();
#endif
        }
    }
}