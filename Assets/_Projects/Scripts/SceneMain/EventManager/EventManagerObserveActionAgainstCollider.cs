using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using IA;
using System;
using IvyType = Main.Eventer.Objects.BigIviesClass.Type;
using Key1DoorType = Main.Eventer.Objects.DoorPuzzleSolving.DoorPuzzleSolvingClass.Key1DoorType;
using FinalKey2DoorType = Main.Eventer.Objects.DoorPuzzleSolving.DoorPuzzleSolvingClass.FinalKey2DoorType;

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
                "ActionAgainstCollider/Event/ToiletLockedDoor" => () => OpenToiletLockedDoor(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/ToiletOneWayDoor" => () => OpenToiletOneWayDoor(),
                "ActionAgainstCollider/Event/WarehouseLockedDoor" => () => OpenWarehouseLockedDoor(),
                "ActionAgainstCollider/Event/DeerBlood" => () => CutDeer(),
                "ActionAgainstCollider/Event/CupBlood" => () => ScoopDeerBlood(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/WarehouseOneWayDoor" => () => OpenWarehouseOneWayDoor(),
                "ActionAgainstCollider/Event/KokeshiBlood" => () => PourDeerBlood(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/KokeshiKey" => () => PickUpSecretKey(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/CupCrash" => () => CrashCup(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/SetCrashedCup" => () => ScatterGlassPiece(),
                "ActionAgainstCollider/Event/DoorPuzzleSovingKey" => () => PickUpKeyInDoorPuzzleSolving(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/DoorPuzzleSovingKey1FirstDoorKnob" => () => OpenCaveKey1Door(Key1DoorType.First),
                "ActionAgainstCollider/Event/DoorPuzzleSovingKey1SecondDoorKnob" => () => OpenCaveKey1Door(Key1DoorType.Second),
                "ActionAgainstCollider/Event/DoorPuzzleSovingOneWayDoorKnob" => () => OpenCaveOneWayDoor(),
                "ActionAgainstCollider/Event/DoorPuzzleSovingFinalKey2FirstDoorKnob" => () => OpenCaveFinalKey2Door(FinalKey2DoorType.First),
                "ActionAgainstCollider/Event/DoorPuzzleSovingFinalKey2SecondDoorKnob" => () => OpenCaveFinalKey2Door(FinalKey2DoorType.Second),
                _ => null
            };
        }
    }
}