using Cysharp.Threading.Tasks;
using DG.Tweening;
using General;
using IA;
using Main.Eventer;
using System;
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
            _player.StopPlayerMove();
            _player.SetTransform(_points.Init);
            _player.CheckDeviation(_points.Init, ct).Forget();

            _uiElements.BlackImageAlpha = 1;
            await _uiElements.FadeIn(2, Ease.Linear, ct);
            _player.InitPlayerMove();
            ObserveAction(ct).Forget();
            ObserveTrigger(ct);
            _uiElements.ActivateUIManagers(ct);

            _busMover.MoveOnce(ct).Forget();

            //await UniTask.WaitUntil(() => _borders.BorderHoge.IsIn(_player.Position) == true, cancellationToken: ct);
            await UniTask.Delay(5000, cancellationToken: ct);

            _uiElements.NewlyShowLogText("テスト：エリアに入った", 5);
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

                _uiElements.NewlyShowLogText(text, 3);
            }
        }

        private void ObserveTrigger(CancellationToken ct)
        {
            BusStopCannotMove(ct).Forget();

            async UniTaskVoid BusStopCannotMove(CancellationToken ct)
            {
                while (true)
                {
                    await UniTask.WaitUntil(() => _borders.IsInAny(_player.Position) is false, cancellationToken: ct);
                    await UniTask.WaitUntil(() => _borders.IsInAny(_player.Position) is true, cancellationToken: ct);
                    _uiElements.NewlyShowLogText("真っ暗で、先が見えない…", 5);
                    Debug.Log("!!");

                    await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: ct);
                }
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
}
