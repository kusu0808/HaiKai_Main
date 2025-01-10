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
            PathWayFirstFootOnDish(ct).Forget();
            PathWayCannotGo(ct).Forget();
            VillageWayDeerCry(ct).Forget();
            VillageWayDeerAppear(ct).Forget();
            VillageWayBirdAppear(ct).Forget();
            HouseFeelYatsu(ct).Forget();
            ShrineWayFoundByYatsu(ct).Forget();
            FromShrineUpWayToVillageFarWayEscapeFromYatsu(ct).Forget();
            VillageFarWayYatsuStopToiletKnock(ct).Forget();
            WarehouseDeerFall(ct).Forget();
            VillageFarWayYatsuDaughterVoice(ct).Forget();
            VillageFarWayCutIvyYatsuComeFromCave(ct).Forget();
            VillageFarWayYatsuStepOnGlassPiece(ct).Forget();
            CaveGokiChanAppear(ct).Forget();
            CaveExitYatsuVoice(ct).Forget();
            ShrineUpWayYatsuAppearAtLastEscape(ct).Forget();
            BusStopEscape(ct).Forget();

            // 繰り返し
            PlayWalkingSounds(ct).Forget();
            EnableGoUpStairs(ct).Forget();
            BusStopCannotMove(ct).Forget();
            PathWaySquat(ct).Forget();
            UnderStageSquat(ct);
            VillageWayCannotGoBackAfterWarehouse(ct).Forget();
            ShrineUpWayDaughterSaysNotHere(ct).Forget();
            PathWayDaughterSaysNotHere(ct).Forget();
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