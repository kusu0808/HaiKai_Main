using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid BusStopEscape(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _isPickUpSecretKeyEventEnabled is false, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.BusStopEscape.IsIn(_player.Position) is true, cancellationToken: ct);

            //ヤツが車に轢かれるムービー再生
            //リザルトシーンでエンドロール表示"
            Scene.ID.Result.LoadAsync().Forget();
        }
    }
}