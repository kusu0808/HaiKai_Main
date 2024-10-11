using Cysharp.Threading.Tasks;
using DG.Tweening;
using IA;
using Main.Eventer;
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
            _uiElements.ActivateUIManagers(ct);

            _busMover.MoveOnce(ct).Forget();

            await UniTask.WaitUntil(() => _borders.BorderHoge.IsIn(_player.Position) == true, cancellationToken: ct);

            _uiElements.NewlyShowLogText("テスト：エリアに入った", 5);
        }

        private async UniTaskVoid ObserveAction(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => InputGetter.Instance.PlayerAction.Bool, cancellationToken: ct);

                Collider collider = _player.GetHitColliderFromCamera();

                if (collider == null) continue;  // 当たらなかった

                string text = collider.tag switch
                {
                    "ActionEvent/BusSign" => "古びた標識だ",
                    _ => string.Empty
                };

                if (string.IsNullOrEmpty(text)) continue;  // 無効なものに当たった

                _uiElements.NewlyShowLogText(text, 3);
            }
        }
    }
}