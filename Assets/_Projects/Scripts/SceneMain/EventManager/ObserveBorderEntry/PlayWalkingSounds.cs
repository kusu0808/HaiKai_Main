using BorderSystem;
using Cysharp.Threading.Tasks;
using General;
using Main.Eventer.Borders;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void PlayWalkingSounds(CancellationToken ct)
        {
            PlayWalkingSound(_borders.WalkingSounds.BrokenDish, _audioClips.BGM.WalkOnBrokenDish, ct).Forget();
            PlayWalkingSound(_borders.WalkingSounds.Bridge, _audioClips.BGM.WalkOnBridge, ct).Forget();
            PlayWalkingSound(_borders.WalkingSounds.Corridor, _audioClips.BGM.WalkOnCorridor, ct).Forget();
            PlayWalkingSound(_borders.WalkingSounds.Tatami, _audioClips.BGM.WalkOnTatami, ct).Forget();
            PlayWalkingSound(_borders.WalkingSounds.Road, _audioClips.BGM.WalkOnRoad, ct).Forget();
            PlayWalkingSound(_borders.WalkingSounds.StoneStairs, _audioClips.BGM.WalkOnStoneStairs, ct).Forget();
            PlayWalkingSound(_borders.WalkingSounds.Soil, _audioClips.BGM.WalkOnSoil, ct).Forget();

            async UniTaskVoid PlayWalkingSound(MultiBorders borders, AudioClip audioClip, CancellationToken ct, SoundType soundType = SoundType.BGM)
            {
                if (borders is null) return;
                if (audioClip == null) return;

                bool IsInAndMoving() => borders.IsInAny(_player.Position) && _player.IsMoving;

                while (true)
                {
                    await UniTask.WaitUntil(() => IsInAndMoving() is true, cancellationToken: ct);
                    AudioSource audioSource = _audioSources.GetNew();
                    audioSource.Raise(audioClip, soundType);
                    await UniTask.WaitUntil(() => IsInAndMoving() is false, cancellationToken: ct);
                    audioSource.Stop();
                }
            }
        }
    }
}