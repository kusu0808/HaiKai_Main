using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    // Borderを使うイベント
    public sealed partial class EventManager
    {
        private void ObserveBorderEntry(CancellationToken ct)
        {
            // 一回だけ
            FootOnDishOnce(ct).Forget();
            PathWayCannotGo(ct).Forget();
            VillageWayDeerCry(ct).Forget();
            VillageWayDeerAppear(ct).Forget();
            VillageWayBirdAppear(ct).Forget();
            HouseFeelYatsu(ct).Forget();
            ShrineWayFoundedByYatsu(ct).Forget();
            FromShrineUpWayToVillageFarWayEscapeFromYatsu(ct).Forget();
            RunIntoToilet(ct).Forget();
            VillageFarWayYatsuStopToiletKnock(ct).Forget();
            WarehouseDeerFall(ct).Forget();
            VillageFarWayYatsuDaughterVoice1(ct).Forget();
            VillageFarWayYatsuDaughterVoice2(ct).Forget();
            VillageFarWayCutIvyYatsuComeFromCave(ct).Forget();

            // 繰り返し
            BusStopCannotMove(ct).Forget();
            FootOnDish(ct).Forget();
            BridgePlaySound(ct).Forget();
            PathWaySquat(ct).Forget();
            EnableGoUpOnEnteringHouse(ct).Forget();
            HousePlayWalkCorridorSound(ct).Forget();
            HousePlayWalkTatamiSound(ct).Forget();
            UnderStageSquat(ct);
            EnableGoUpOnShrineWay(ct).Forget();
            EnableGoUpOnWarehouseStairs(ct).Forget();
            VillageWayCannotGoBackAfterWarehouse(ct).Forget();
            VillageFarWayYatsuStepOnGlassPiece(ct).Forget();
        }

        // 後方置換されるかも
        async UniTask _TeleportPlayer(Transform transform, CancellationToken ct)
        {
            if (transform == null) return;
            _player.IsPlayerControlEnabled = false;
            await _uiElements.BlackImage.FadeOut(EventManagerConst.FadeOutDuration, ct);
            _player.SetTransform(transform);
            await _uiElements.BlackImage.FadeIn(EventManagerConst.FadeInDuration, ct);
            _player.IsPlayerControlEnabled = true;
        }
    }
}