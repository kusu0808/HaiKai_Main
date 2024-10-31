using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using IA;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        // コライダーを使うイベント：Rayを飛ばす
        private async UniTaskVoid ObserveAction(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.Yield(ct);

                if (PauseState.IsPaused is true) continue;  // ポーズ中
                await UniTask.WaitUntil(() => InputGetter.Instance.PlayerAction.Bool, cancellationToken: ct);
                if (PauseState.IsPaused is true) continue;  // ポーズ中

                Collider collider = _player.GetHitColliderFromCamera();

                if (collider == null) continue;  // 当たらなかった

                string text = collider.tag.GetMessage();

                if (string.IsNullOrEmpty(text)) continue;  // 無効なものに当たった

                _uiElements.NewlyShowLogText(text, EventManagerConst.NormalTextShowDuration);
            }
        }
    }
}