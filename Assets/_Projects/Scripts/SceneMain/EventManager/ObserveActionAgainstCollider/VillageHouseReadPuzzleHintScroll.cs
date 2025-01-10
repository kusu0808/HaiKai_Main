using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageHouseReadPuzzleHintScroll(CancellationToken ct)
        {
            PauseState.IsPaused = true;
            "巻物を読み始める".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "巻物を読み終わる".Warn();
            PauseState.IsPaused = false;
        }
    }
}