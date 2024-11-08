using Cysharp.Threading.Tasks;
using System.Threading;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        // 初期化
        // バスの動きなど、行動の開始もここで
        private async UniTask Initialize(CancellationToken ct)
        {
            _playerCollision.Init(col => OnPlayerTriggerEnter(col, ct));

            _uiElements.SetCursor(false);
            _player.IsPlayerControlEnabled = false;
            _player.SetTransform(_points.Init);
            _player.SlopLimit = EventManagerConst.SlopLimitInit;
            _player.CheckDeviation(_points.Init, ct).Forget();
            _uiElements.IsShowDaughterKnife = false;

            if (_debug.IsEnabled) InitializeDebugProperty();

            await _uiElements.FadeIn(EventManagerConst.FadeInDuration, ct);
            _player.IsPlayerControlEnabled = true;

            _uiElements.ActivateUIManagers(ct);
            _busMover.MoveOnce(ct).Forget();
        }

        private void InitializeDebugProperty()
        {
            "デバッグ機能が有効になっています".Warn();

            if (_debug.FastMove) _player.FastenPlayer();
            if (_debug.FastLook) _player.FastenLook();
        }
    }
}