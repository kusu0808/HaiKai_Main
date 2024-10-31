using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid TMPNAME_ReadMemo(CancellationToken ct)
        {
            PauseState.IsPaused = true;
            "メモを読み始める".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "メモを読み終わる".Warn();
            PauseState.IsPaused = false;
        }
    }
}