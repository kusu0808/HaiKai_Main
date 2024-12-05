using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ScoopDeerBlood(CancellationToken ct)
        {
            if (_uiElements.Cup.IsHolding() is true)
            {
                _uiElements.Cup.Release();

                PauseState.IsPaused = true;
                "血入りコップ入手開始".Warn();
                await UniTask.Delay(1000, ignoreTimeScale: true, cancellationToken: ct);
                "血入りコップ入手終了".Warn();
                PauseState.IsPaused = false;

                _uiElements.CupFilledWithBlood.Obtain();
            }
        }
    }
}