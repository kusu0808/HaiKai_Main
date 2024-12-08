using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using IA;
using System;
using IvyType = Main.Eventer.Objects.BigIviesClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        // コライダーを使うイベント：Rayを飛ばす
        private async UniTaskVoid ObserveActionAgainstCollider(CancellationToken ct)
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

                Action @event = GetEvent(tag, ct);
                if (@event is not null) @event.Invoke(); // イベントが発火したので、ログは出さない
                else
                {
                    string message = GetMessage(tag);
                    if (string.IsNullOrEmpty(message)) continue;  // 無効なものに当たった
                    _uiElements.LogText.ShowAutomatically(message);
                }
            }

            static string GetMessage(string tag) => tag switch
            {
                "ActionAgainstCollider/Message/BusSign" => "古びた標識だ",
                "ActionAgainstCollider/Message/PathWaySign" => "汚れていて見えない",
                "ActionAgainstCollider/Message/ClosedDoor" => "開かない",
                "ActionAgainstCollider/Message/LockedDoor" => "鍵がかかっている",
                _ => string.Empty
            };

            Action GetEvent(string tag, CancellationToken ctIfNeeded) => tag switch
            {
                "ActionAgainstCollider/Event/DaughterKnife" => () => PickUpDaughterKnife(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/BigIvyOnPathWay" => () => CutBigIvy(IvyType.PathWay),
                "ActionAgainstCollider/Event/BigIvyOnShrineStair" => () => CutBigIvy(IvyType.ShrineStair),
                "ActionAgainstCollider/Event/BigIvyOnCaveEntrance" => () => CutBigIvy(IvyType.CaveEntrance),
                "ActionAgainstCollider/Event/PuzzleHintScroll" => () => ReadPuzzleHintScroll(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/ButaisideKey" => () => PickUpButaiSideKey(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/Cup" => () => PickUpCup(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/ToiletOneWayDoor" => () => OpenToiletOneWayDoor(),
                "ActionAgainstCollider/Event/WarehouseLockedDoor" => () => OpenWarehouseLockedDoor(),
                "ActionAgainstCollider/Event/DeerBlood" => () => CutDeer(),
                "ActionAgainstCollider/Event/CupBlood" => () => ScoopDeerBlood(ctIfNeeded).Forget(),
                _ => null
            };
        }
    }
}