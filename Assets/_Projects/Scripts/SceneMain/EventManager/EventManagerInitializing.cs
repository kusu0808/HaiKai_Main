using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using General;
using Main.Eventer.UIElements;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        // 初期化
        // バスの動きなど、行動の開始もここで
        private async UniTask Initialize(CancellationToken ct)
        {
            _uiElements.Init();
            _daughter.InitNavMeshAgent();
            _yatsu.InitNavMeshAgent();

            _uiElements.Reticle.Color = ReticleClass.ColorNormal;
            _uiElements.Reticle.Size = ReticleClass.SizeDefault;
            _uiElements.Reticle.IsInvisible = false;
            _player.IsCameraEaseCut = false;
            _player.IsPlayerControlEnabled = false;
            _player.SetTransform(_points.Init);
            _player.SlopLimit = EventManagerConst.SlopLimitInit;
            _player.CheckDeviation(_points.Init, ct).Forget();
            _player.SubscribeYatsuCollision();
            _daughter.SpawnHere(_points.RoadWayDaughterSpawnPoint);
            _player.SubscribeGrounded(PlayGroundedSound);
            TriggerPauseUI.OnPauseBegin.Subscribe(_ => _uiElements.Reticle.IsInvisible = true).AddTo(gameObject);
            TriggerPauseUI.OnPauseEnd.Subscribe(_ => _uiElements.Reticle.IsInvisible = false).AddTo(gameObject);
            _yatsu.ChasedBGM = _audioClips.BGM.ChasedByYatsu;

            if (_debug.IsEnabled) InitializeDebugProperty(ct).Forget();

            await _uiElements.BlackImage.FadeIn(EventManagerConst.FadeInDuration, ct);
            _player.IsPlayerControlEnabled = true;

            _busMover.MoveOnce(ct).Forget();
        }

        private async UniTaskVoid InitializeDebugProperty(CancellationToken ct)
        {
            "デバッグ機能が有効になっています".Warn();

            await UniTask.DelayFrame(5, cancellationToken: ct); // ポーズスクリプトのイベントバインドが終わるまで、十分待つ
            if (_debug.FastMove) _player.FastenPlayer();
            if (_debug.FastLook) _player.FastenLook();
        }
    }
}