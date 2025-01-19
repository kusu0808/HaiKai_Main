using System.Threading;
using Cysharp.Threading.Tasks;
using IA;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageHouseReadPuzzleHintScroll(CancellationToken ct)
        {
            PauseState.IsPaused = true;
            _uiElements.KokeshiScroll.IsEnabled = true;
            await UniTask.WaitUntil(() => InputGetter.Instance.PlayerAction.Bool, cancellationToken: ct);
            _uiElements.KokeshiScroll.IsEnabled = false;
            PauseState.IsPaused = false;
        }
    }
}