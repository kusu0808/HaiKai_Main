using System.Threading;
using Cysharp.Threading.Tasks;
using IA;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageHouseReadPuzzleHintScroll(CancellationToken ct)
        {
            TriggerPauseUI.IsInputEnabled = false;
            PauseState.IsPaused = true;
            _uiElements.KokeshiScroll.IsEnabled = true;
            await UniTask.WhenAny(
                UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool, cancellationToken: ct),
                UniTask.WaitUntil(() => InputGetter.Instance.Pause.Bool, cancellationToken: ct)
            );
            await UniTask.NextFrame(cancellationToken: ct);
            _uiElements.KokeshiScroll.IsEnabled = false;
            PauseState.IsPaused = false;
            TriggerPauseUI.IsInputEnabled = true;
        }
    }
}