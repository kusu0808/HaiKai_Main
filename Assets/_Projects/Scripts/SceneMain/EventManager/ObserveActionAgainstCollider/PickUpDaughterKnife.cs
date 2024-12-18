using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PickUpDaughterKnife(CancellationToken ct)
        {
            if (_daughter.IsKnifeEnabled is false) return;

            PauseState.IsPaused = true;
            "ナイフのUI表示開始".Warn();
            await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
            "ナイフのUI表示終了".Warn();
            PauseState.IsPaused = false;

            _playerItem.HasKnife = true;
            _daughter.IsKnifeEnabled = false;
            _uiElements.DaughterKnife.IsShow = true;
        }
    }
}