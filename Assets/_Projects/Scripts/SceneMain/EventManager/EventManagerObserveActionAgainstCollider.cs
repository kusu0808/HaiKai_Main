using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using IA;
using System;
using IvyType = Main.Eventer.Objects.BigIviesClass.Type;
using Key1DoorType = Main.Eventer.Objects.DoorPuzzleSolving.Key1Door.Type;
using FinalKey2DoorType = Main.Eventer.Objects.DoorPuzzleSolving.FinalKey2Door.Type;
using ChainType = Main.Eventer.Objects.DaughterChainClass.Type;

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
                "ActionAgainstCollider/Event/DaughterKnife" => () => PathWayPickUpDaughterKnife(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/BigIvyOnPathWay" => () => GeneralCutBigIvy(IvyType.PathWay),
                "ActionAgainstCollider/Event/BigIvyOnShrineStair" => () => GeneralCutBigIvy(IvyType.ShrineStair),
                "ActionAgainstCollider/Event/BigIvyOnCaveEntrance" => () => GeneralCutBigIvy(IvyType.CaveEntrance),
                "ActionAgainstCollider/Event/PuzzleHintScroll" => () => VillageHouseReadPuzzleHintScroll(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/ButaisideKey" => () => ShrineUpWayPickUpButaiSideKey(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/Cup" => () => VillageToiletPickUpCup(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/ToiletLockedDoor" => () => VillageToiletOpenLockedDoor(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/ToiletOneWayDoor" => () => VillageToiletOpenOneWayDoor(),
                "ActionAgainstCollider/Event/WarehouseLockedDoor" => () => WarehouseOpenLockedDoor(),
                "ActionAgainstCollider/Event/DeerBlood" => () => WarehouseCutDeer(),
                "ActionAgainstCollider/Event/CupBlood" => () => WarehouseScoopDeerBlood(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/WarehouseOneWayDoor" => () => WarehouseOpenOneWayDoor(),
                "ActionAgainstCollider/Event/KokeshiBlood" => () => WarehousePourDeerBlood(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/KokeshiKey" => () => VillageHousePickUpSecretKey(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/CupCrash" => () => VillageFarWayCrashCup(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/SetCrashedCup" => () => VillageFarWayScatterGlassPiece(),
                "ActionAgainstCollider/Event/DoorPuzzleSovingKey" => () => CavePickUpKeyInDoorPuzzleSolving(ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/DoorPuzzleSovingKey1FirstDoorKnob" => () => CaveOpenKey1Door(Key1DoorType.First),
                "ActionAgainstCollider/Event/DoorPuzzleSovingKey1SecondDoorKnob" => () => CaveOpenKey1Door(Key1DoorType.Second),
                "ActionAgainstCollider/Event/DoorPuzzleSovingOneWayDoorKnob" => () => CaveOpenOneWayDoor(),
                "ActionAgainstCollider/Event/DoorPuzzleSovingFinalKey2RightDoorKnob" => () => CaveOpenFinalKey2Door(FinalKey2DoorType.Right),
                "ActionAgainstCollider/Event/DoorPuzzleSovingFinalKey2LeftDoorKnob" => () => CaveOpenFinalKey2Door(FinalKey2DoorType.Left),
                "ActionAgainstCollider/Event/DaughterChain1" => () => ShrineCutDaughterChain(ChainType.Chain1, ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/DaughterChain2" => () => ShrineCutDaughterChain(ChainType.Chain2, ctIfNeeded).Forget(),
                "ActionAgainstCollider/Event/DaughterChain3" => () => ShrineCutDaughterChain(ChainType.Chain3, ctIfNeeded).Forget(),
                _ => null
            };
        }
    }
}