using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWayCannotGo(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.PathWayStop.IsIn(_player.Position) is true, cancellationToken: ct);
            _uiElements.LogText.ShowAutomatically("この先は高くて進めそうにない");
        }
    }
}