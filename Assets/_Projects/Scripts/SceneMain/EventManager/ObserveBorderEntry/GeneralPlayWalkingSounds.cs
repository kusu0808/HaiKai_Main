using Cysharp.Threading.Tasks;
using General;
using Main.Eventer.Borders;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

// ゲームを開始してすぐに終了すると、なぜか次がスローされる：「GameObjects can not be made active when they are being destroyed.」

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid GeneralPlayWalkingSounds(CancellationToken ct)
        {
            Func<MultiBorders, bool> getIsInAndMoving = (borders) =>
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

            WalkingSound asphalt = new WalkingSound(
                    MultiBorders.New(_borders.WalkingSounds.Road, _borders.WalkingSounds.StoneStairs),
                    getIsInAndMoving, _audioClips.BGM.WalkOnAsphalt, gameObject
                )
                .AddTo(gameObject);
            WalkingSound soil = new WalkingSound(
                    MultiBorders.New(_borders.WalkingSounds.Soil),
                    getIsInAndMoving, _audioClips.BGM.WalkOnSoil, gameObject
                )
                .AddTo(gameObject);
            WalkingSound corridor = new WalkingSound(
                    MultiBorders.New(_borders.WalkingSounds.Bridge, _borders.WalkingSounds.Corridor),
                    getIsInAndMoving, _audioClips.BGM.WalkOnCorridor, gameObject
                )
                .AddTo(gameObject);

            _isWalkingSoundMuted.Subscribe(value =>
            {
                asphalt.IsMuted = value;
                soil.IsMuted = value;
                corridor.IsMuted = value;
            });

            while (true)
            {
                CheckPriority(new WalkingSound[] { corridor, asphalt, soil });
                await UniTask.NextFrame(ct);
            }

            static void CheckPriority(WalkingSound[] priority, int head = 0)
            {
                if (priority is null) return;

                int len = priority.Length;
                if (head >= len) return;

                WalkingSound curr = priority[head];
                curr.IsPressed = false;
                if (head == len - 1) return;

                if (curr.IsPlaying)
                {
                    for (int i = head + 1; i < len; i++)
                    {
                        priority[i].IsPressed = true;
                    }
                }
                else
                {
                    CheckPriority(priority, head + 1);
                }
            }
        }

        private void PlayGroundedSound() => _audioSources.GetNew().Raise(_audioClips.SE.Grounded, SoundType.SE);

        private sealed class WalkingSound : IDisposable
        {
            private MultiBorders _borders;
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

            public bool IsMuted
            {
                set
                {
                    if (_audioSource == null) return;
                    _audioSource.mute = value;
                }
            }

            public WalkingSound(
                MultiBorders borders,
                Func<MultiBorders, bool> getIsInAndMoving, AudioClip audioClip, GameObject audioSourceRoot
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

            private async UniTask ObserveMovingInBorder(Func<MultiBorders, bool> getIsInAndMoving, CancellationToken ct)
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