using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using IA;
using System;

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

                string tag = collider.tag;

                Action action = GetAction(tag, ct);
                if (action is not null) action.Invoke(); // イベントが発火したので、ログは出さない
                else
                {
                    string message = GetMessage(tag);
                    if (string.IsNullOrEmpty(message)) continue;  // 無効なものに当たった
                    _uiElements.NewlyShowLogText(message, EventManagerConst.NormalTextShowDuration);
                }
            }
        }

        private string GetMessage(string tag) => tag switch
        {
            "ActionEvent/BusSign" => "古びた標識だ",
            "ActionEvent/PathWaySign" => "汚れていて見えない",
            _ => string.Empty
        };

        private Action GetAction(string tag, CancellationToken ctIfNeeded) => tag switch
        {
            "StoryEvent/DaughterKnife" => () => PickUpDaughterKnife(ctIfNeeded).Forget(),
            "StoryEvent/BigIvy" => () => CutBigIvy(),
            _ => null
        };
    }
}