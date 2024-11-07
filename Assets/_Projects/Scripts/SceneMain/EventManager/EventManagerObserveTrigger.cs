using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    // Borderを使うイベント
    public sealed partial class EventManager
    {
        private void ObserveTrigger(CancellationToken ct)
        {
            // 一回だけ
            FootOnDishOnce(ct).Forget();
            PathWayCannotGo(ct).Forget();
            VillageWayDeerCry(ct).Forget();

            // 繰り返し
            BusStopCannotMove(ct).Forget();
            FootOnDish(ct).Forget();
            BridgePlaySound(ct).Forget();
            PathWaySquat(ct).Forget();
            EnableGoUpOnEnteringHouse(ct).Forget();
            UnderStageSquat(ct).Forget();
        }

        // 後方置換されるかも
        async UniTask _TeleportPlayer(Transform transform, CancellationToken ct)
        {
            if (transform == null) return;
            _player.IsPlayerControlEnabled = false;
            await _uiElements.FadeOut(EventManagerConst.FadeOutDuration, ct);
            _player.SetTransform(transform);
            await _uiElements.FadeIn(EventManagerConst.FadeInDuration, ct);
            _player.IsPlayerControlEnabled = true;
        }
    }
}