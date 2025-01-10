using BorderSystem;
using Cysharp.Threading.Tasks;
using General;
using Main.Eventer.Borders;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using UniRx;
using UnityEngine;

// ゲームを開始してすぐに終了すると、なぜか次がスローされる：「GameObjects can not be made active when they are being destroyed.」

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PlayWalkingSounds(CancellationToken ct)
        {
            Func<ReadOnlyCollection<Border>, bool> getIsInAndMoving = (borders) =>
            {
                if (borders is null) return false;

                if (_player.IsMoving is false) return false;

                if (_borders.IsFromUnderStageToShrineWayBorderEnabled)
                {
                    return borders.IsInAny(
                        _player.Position,
                        EventManagerConst.WalkingSoundBorderLayerGeneral, EventManagerConst.WalkingSoundBorderLayerPathWayBridge
                    );
                }
                else
                {
                    return borders.IsInAny(
                        _player.Position,
                        EventManagerConst.WalkingSoundBorderLayerGeneral
                    );
                }
            };

            WalkingSound asphalt =
                new WalkingSound(
                    MultiBorders.JoinAll(_borders.WalkingSounds.Road, _borders.WalkingSounds.StoneStairs),
                    getIsInAndMoving, _audioClips.BGM.WalkOnAsphalt, gameObject)
                .AddTo(gameObject);
            WalkingSound soil =
                new WalkingSound(
                    MultiBorders.JoinAll(_borders.WalkingSounds.Soil),
                    getIsInAndMoving, _audioClips.BGM.WalkOnSoil, gameObject)
                .AddTo(gameObject);
            WalkingSound corridor =
                new WalkingSound(
                    MultiBorders.JoinAll(_borders.WalkingSounds.Bridge, _borders.WalkingSounds.Corridor),
                    getIsInAndMoving, _audioClips.BGM.WalkOnCorridor, gameObject)
                .AddTo(gameObject);

            try
            {
                while (true)
                {
                    corridor.IsPressed = false; // 第一優先

                    if (corridor.IsPlaying) // 他の音は鳴らしてはいけない
                    {
                        asphalt.IsPressed = true;
                        soil.IsPressed = true;
                    }
                    else
                    {
                        asphalt.IsPressed = false; // 第二優先

                        if (asphalt.IsPlaying) // 他の音は鳴らしてはいけない
                        {
                            soil.IsPressed = true;
                        }
                        else
                        {
                            soil.IsPressed = false; // 第三優先
                        }
                    }

                    await UniTask.NextFrame(ct);
                }
            }
            catch (OperationCanceledException) { } // なぜかこれがスローされる
        }

        private void PlayGroundedSound() => _audioSources.GetNew().Raise(_audioClips.SE.Grounded, SoundType.SE);

        private sealed class WalkingSound : IDisposable
        {
            private ReadOnlyCollection<Border> _borders;
            private AudioClip _audioClip;
            private AudioSource _audioSource;
            private readonly CancellationTokenSource cts = new();

            // クラスの外部からのみ操作：trueなら、↓のフラグがtrueになることはない
            public bool IsPressed { get; set; } = false;
            // クラスの内部からのみ操作(getIsInAndMovingによってのみ変更される)：サウンドの再生状態と連動(同じ値を設定してもOK)
            private readonly ReactiveProperty<bool> _isPlaying = new(false);
            public bool IsPlaying
            {
                get => _isPlaying.Value;
                private set => _isPlaying.Value = IsPressed ? false : value;
            }

            public WalkingSound(
                ReadOnlyCollection<Border> borders,
                Func<ReadOnlyCollection<Border>, bool> getIsInAndMoving, AudioClip audioClip, GameObject audioSourceRoot
            )
            {
                _borders = borders;
                _audioClip = audioClip;
                _audioSource = audioSourceRoot.AddComponent<AudioSource>(); // ずっと使い続けるので、特別に生成

                ObserveMovingInBorder(getIsInAndMoving, cts.Token).Forget();

                _isPlaying.Subscribe(value =>
                {
                    if (value)
                    {
                        if (_audioSource == null) return;
                        _audioSource.Raise(_audioClip, SoundType.BGM);
                    }
                    else
                    {
                        if (_audioSource == null) return;
                        _audioSource.Stop();
                    }
                });
            }

            private async UniTask ObserveMovingInBorder(Func<ReadOnlyCollection<Border>, bool> getIsInAndMoving, CancellationToken ct)
            {
                while (true)
                {
                    await UniTask.WaitUntil(() => getIsInAndMoving(_borders) is true, cancellationToken: ct);
                    _isPlaying.Value = true; // IsPressedによって、trueにならないこともある
                    await UniTask.WaitUntil(() => getIsInAndMoving(_borders) is false, cancellationToken: ct);
                    _isPlaying.Value = false;
                }
            }

            public void Dispose()
            {
                cts?.Cancel();
                cts?.Dispose();
                _isPlaying?.Dispose();

                Destroy(_audioSource);
                _audioSource = null;

                _borders = null;
                _audioClip = null;
            }
        }
    }
}