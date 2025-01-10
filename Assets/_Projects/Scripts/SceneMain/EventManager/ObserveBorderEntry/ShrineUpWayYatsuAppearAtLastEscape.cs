using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ShrineUpWayYatsuAppearAtLastEscape(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _isPickUpSecretKeyEventEnabled is false, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.ShrineUpWayYatsuAppearAtLastEscape.IsIn(_player.Position) is true, cancellationToken: ct);
            _yatsu.SpawnHere(_points.ShrineUpWayYatsuComeAtLastEscapeSpawnPoint);
        }
    }
}