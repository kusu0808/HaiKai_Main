using Cysharp.Threading.Tasks;
using System.Threading;
using BorderSystem;
using General;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid BridgePlaySound(CancellationToken ct)
        {
            Border cache = _borders.BridgePlaySound;

            bool IsMovingOnBridge() => cache.IsIn(_player.Position) is true && _player.IsMoving is true;

            while (true)
            {
                await UniTask.WaitUntil(() => IsMovingOnBridge() is true, cancellationToken: ct);
                AudioSource audioSource = _audioSources.GetNew();
                audioSource.Raise(_audioClips.BGM.BridgeCreak, SoundType.BGM);
                await UniTask.WaitUntil(() => IsMovingOnBridge() is false, cancellationToken: ct);
                audioSource.Stop();
            }
        }
    }
}