using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PickUpDaughterKnife(CancellationToken ct)
        {
            PauseState.IsPaused = true;
            "ナイフのUI表示開始".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "ナイフのUI表示終了".Warn();
            PauseState.IsPaused = false;

            _playerItem.HasKnife = true;
            _daughter.SetKnifeEnabled(false);
            _uiElements.IsShowDaughterKnife = true;
        }
    }
}