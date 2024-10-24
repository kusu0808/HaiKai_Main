using BorderSystem;
using Cysharp.Threading.Tasks;
using General;
using IA;
using Main.Eventer;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    public sealed class EventManager : MonoBehaviour
    {
        [SerializeField] private Points _points;
        [SerializeField] private Borders _borders;
        [SerializeField] private Player _player;
        [SerializeField] private UIElements _uiElements;
        [Space(25)]
        [SerializeField] private BusMover _busMover;

        private void OnEnable() => Observe(destroyCancellationToken).Forget();

        private async UniTaskVoid Observe(CancellationToken ct)
        {
            _uiElements.SetCursor(false);
            _player.IsPlayerControlEnabled = false;
            _player.SetTransform(_points.Init);
            _player.CheckDeviation(_points.Init, ct).Forget();

            await _uiElements.FadeIn(EventManagerConst.FadeInDuration, ct);
            _player.IsPlayerControlEnabled = true;
            ObserveAction(ct).Forget();
            ObserveTrigger(ct);
            _uiElements.ActivateUIManagers(ct);

            _busMover.MoveOnce(ct).Forget();
        }

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

        private void ObserveTrigger(CancellationToken ct)
        {
            BusStopCannotMove(ct).Forget();
            BridgePlaySound(ct).Forget();
            PathWaySquat(ct).Forget();

            async UniTaskVoid BusStopCannotMove(CancellationToken ct)
            {
                ReadOnlyCollection<Border> cache = _borders.BusStopCannotMove;

                while (true)
                {
                    await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is false, cancellationToken: ct);
                    await UniTask.WaitUntil(() => cache.IsInAny(_player.Position) is true, cancellationToken: ct);
                    _uiElements.NewlyShowLogText("真っ暗で、先が見えない…", EventManagerConst.NormalTextShowDuration);
                    await UniTask.Delay(TimeSpan.FromSeconds(EventManagerConst.SameEventDuration), cancellationToken: ct);
                }
            }

            async UniTaskVoid BridgePlaySound(CancellationToken ct)
            {
                Border cache = _borders.BridgePlaySound;

                while (true)
                {
                    await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is true, cancellationToken: ct);
                    "後方置換：橋がきしむ音を再生開始".Warn();
                    await UniTask.WaitUntil(() => cache.IsIn(_player.Position) is false, cancellationToken: ct);
                    "後方置換：橋がきしむ音を再生終了".Warn();
                }
            }

            async UniTaskVoid PathWaySquat(CancellationToken ct)
            {
                while (true) await UniTask.WhenAny(
                        PathWaySquatImpl(_borders.PathWaySquat1, ct), PathWaySquatImpl(_borders.PathWaySquat2, ct));

                async UniTask PathWaySquatImpl(Borders.TeleportBorder cache, CancellationToken ct)
                {
                    await UniTask.WaitUntil(() => cache.In.IsIn(_player.Position) is true, cancellationToken: ct);
                    _uiElements.ForciblyShowLogText("アクション長押しで通る");
                    int i = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => cache.In.IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool));
                    _uiElements.ForciblyShowLogText(string.Empty);
                    if (i != 1) return;
                    await _TeleportPlayer(cache.FirstTf, ct);
                    await UniTask.WaitUntil(() => cache.Out.IsIn(_player.Position) is true, cancellationToken: ct);
                    await _TeleportPlayer(cache.SecondTf, ct);
                }
            }

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

    public static class EventManagerEx
    {
        public static string GetMessage(this string tag) => tag switch
        {
            "ActionEvent/BusSign" => "古びた標識だ",
            _ => string.Empty
        };
    }

    public static class EventManagerConst
    {
        public static readonly float FadeInDuration = 2;
        public static readonly float FadeOutDuration = 2;
        public static readonly float FadeInOutInterval = 1;
        public static readonly float NormalTextShowDuration = 3;
        public static readonly float EventTextShowDuration = 5;
        public static readonly float SameEventDuration = 5;
        public static readonly float RayMaxDistance = 2;
    }
}
