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
        [SerializeField] private Debug _debug;
        [Space(25)]
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

            if (_debug.IsEnabled) InitializeDebugProperty();

            await _uiElements.FadeIn(EventManagerConst.FadeInDuration, ct);
            _player.IsPlayerControlEnabled = true;
            ObserveAction(ct).Forget();
            ObserveTrigger(ct);
            _uiElements.ActivateUIManagers(ct);

            _busMover.MoveOnce(ct).Forget();
        }

        private void InitializeDebugProperty()
        {
            "デバッグ機能が有効になっています".Warn();

            if (_debug.FastMove) _player.FastenPlayer();
            if (_debug.FastLook) _player.FastenLook();
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

                bool isGetOffInput = 
                    text != "ActionEvent/OneWayDoor";//メッセージの表示終了を３秒後のフェードアウトに限定する

                _uiElements.NewlyShowLogText(text, EventManagerConst.NormalTextShowDuration, isGetOffInput);

                if (text == "ActionEvent/Memo") PosePlayerAsync(ct).Forget();
            }

            async UniTaskVoid PosePlayerAsync(CancellationToken ct)
            {
                try
                {
                    _player.IsPlayerControlEnabled = false;
                    PauseState.IsPaused = true;
                    await UniTask.Delay(TimeSpan.FromSeconds(EventManagerConst.NormalTextShowDuration), cancellationToken: ct);
                    _player.IsPlayerControlEnabled = true;
                    PauseState.IsPaused = false;
                }
                catch (OperationCanceledException)
                {
                    //キャンセルされたときが起きた時ポーズ解除
                    _player.IsPlayerControlEnabled = true;
                    PauseState.IsPaused = false;
                }
            }
        }

        private void ObserveTrigger(CancellationToken ct)
        {
            BusStopCannotMove(ct).Forget();
            BridgePlaySound(ct).Forget();
            PathWaySquat(ct).Forget();
            UnderStageSquat(ct).Forget();

            async UniTaskVoid BusStopCannotMove(CancellationToken ct)
            {
                ReadOnlyCollection<Border> cache = _borders.BusStopCannotMove.Elements;

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
                while (true)
                {
                    int i = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => _borders.PathWaySquat1.In.IsIn(_player.Position) is true, cancellationToken: ct),
                        UniTask.WaitUntil(() => _borders.PathWaySquat2.In.IsIn(_player.Position) is true, cancellationToken: ct));

                    Borders.TeleportBorder cache = i is 0 ? _borders.PathWaySquat1 : _borders.PathWaySquat2;
                    string logText = i is 0 ? "ここ、すごく狭いね (アクション長押しで通る)" : "そろそろ戻ろう (アクション長押しで通る)";

                    _uiElements.ForciblyShowLogText(logText);
                    int j = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => cache.In.IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                    _uiElements.ForciblyShowLogText(string.Empty);
                    if (j is not 1) continue;

                    await _TeleportPlayer(cache.FirstTf, ct);
                    await UniTask.WaitUntil(() => cache.Out.IsIn(_player.Position) is true, cancellationToken: ct);
                    await _TeleportPlayer(cache.SecondTf, ct);
                }
            }

            async UniTaskVoid UnderStageSquat(CancellationToken ct)
            {
                while (true)
                {
                    int i1 = await WaitUntilWithIndex(() => _borders.UnderStageSquat.Elements.IsInInAny(_player.Position), ct);
                    if (i1 is -1) return;
                    var cache1 = _borders.UnderStageSquat.Elements[i1];
                    _uiElements.ForciblyShowLogText("(アクション長押しで入る)");
                    int j1 = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => cache1.In.IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                    _uiElements.ForciblyShowLogText(string.Empty);
                    if (j1 is not 1) continue;
                    await _TeleportPlayer(cache1.OutTf, ct);

                    int i2 = await WaitUntilWithIndex(() => _borders.UnderStageSquat.Elements.IsInOutAny(_player.Position), ct);
                    if (i2 is -1) return;
                    var cache2 = _borders.UnderStageSquat.Elements[i2];
                    _uiElements.ForciblyShowLogText("(アクション長押しで出る)");
                    int j2 = await UniTask.WhenAny(
                        UniTask.WaitUntil(() => cache2.Out.IsIn(_player.Position) is false, cancellationToken: ct),
                        UniTask.WaitUntil(() => InputGetter.Instance.PlayerSpecialAction.Bool, cancellationToken: ct));
                    _uiElements.ForciblyShowLogText(string.Empty);
                    if (j2 is not 1) continue;
                    await _TeleportPlayer(cache2.OutTf, ct);
                }

                /// <summary>
                /// condition が -1 以外を返すまで待機し、そのインデックスを返す
                /// </summary>
                static async UniTask<int> WaitUntilWithIndex(Func<int> condition, CancellationToken ct)
                {
                    if (condition is null) return -1;
                    while (true)
                    {
                        int i = condition();
                        if (i is not -1) return i;
                        await UniTask.Yield(ct);
                    }
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



        [Serializable]
        public sealed class Debug
        {
            [SerializeField, Tooltip("以下の全ての、設定の有効/無効")]
            private bool _isEnabled;
            public bool IsEnabled => _isEnabled;

            [SerializeField, Tooltip("プレイヤーの速度を5倍にする")]
            private bool _fastMove;
            public bool FastMove => _isEnabled && _fastMove;

            [SerializeField, Tooltip("プレイヤーの回転スピードを3倍にする")]
            private bool _fastLook;
            public bool FastLook => _isEnabled && _fastLook;
        }
    }

    public static class EventManagerEx
    {
        public static string GetMessage(this string tag) => tag switch
        {
            "ActionEvent/BusSign" => "古びた標識だ",
            "ActionEvent/PathWaySign" => "汚れていて見えない",
            "ActionEvent/OneWayDoor" => "こちらからは開けられない様だ",
            "ActionEvent/Memo" => "文章未決定",
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
